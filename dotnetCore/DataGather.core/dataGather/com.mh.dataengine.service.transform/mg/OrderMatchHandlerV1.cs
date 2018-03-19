using System;
using System.Linq;
using System.Collections.Generic;

using com.mh.common.extension;
using com.mh.model.mongo.dbMh;
using com.mh.dataengine.common.iservice;
using com.mh.dataengine.common.cache;
using com.mh.common.encrypt;
using com.mh.dataengine.common.helper;

namespace com.mh.dataengine.service.transform.mg
{
    class OrderMatchHandlerV1 : OrderMatchBaseHandler
    {
        public static Dictionary<int, string> WriteData(List<PluginOrderTxt> list, string fileName, List<string> passBrandCodes, out List<string> noSectionCodes)
        {
            noSectionCodes = new List<string>();

            if (list == null || list.Count < 1)
                return new Dictionary<int, string>();



            var errorDic = MDataHelper.Filter<PluginOrderTxt>(list);
            list = list.Where(l => !errorDic.Keys.Contains(l.index)).ToList();
            if (!list.Any())
                return errorDic;

            //拆分优惠券
            list = SplitCoupon(list);

            Log.Info($"SplitCoupon over, list.count : {list.Count}");
            //storeId 暂定为0，忽略storeId查询
            var storeId = 0;
            var sectionCodes = list.Where(l => !string.IsNullOrEmpty(l.sectioncode)).Select(l => l.sectioncode).Distinct().ToList();
            var buyerCodes = list.Where(l => !string.IsNullOrEmpty(l.buyercode)).Select(l => l.buyercode).Distinct().ToList();
            //对多个导购编码进行拆分，取第一个
            buyerCodes = SplitBuyerCode(buyerCodes);
            var couponCodes = list.Where(l => !string.IsNullOrEmpty(l.couponcode)).Select(l => l.couponcode).Distinct().ToList();
            var customerIds = list.Where(l => !string.IsNullOrEmpty(l.customerid)).Select(l => l.customerid).Distinct().ToList();
            var openIds = list.Where(l => !string.IsNullOrEmpty(l.openid)).Select(l => l.openid).Distinct().ToList();

            /*
            var couponList = GetFfCouponList(0, couponCodes);
            var pCouponList = GetPrivilegeCouponList(storeId, couponCodes);
            if (couponList != null && pCouponList != null)
                couponList.AddRange(pCouponList);
            else if (couponList == null)
                couponList = pCouponList;
            */
            var couponList = GetAllCouponList(0, couponCodes);

            var memberInfoIds = couponList.Where(c => !string.IsNullOrEmpty(c.memberId)).Select(c => c.memberId).Distinct().ToList();

            DataCacheV1.AddNewCodes(storeId, sectionCodes, 1);
            DataCacheV1.AddNewCodes(storeId, buyerCodes, 2);
            DataCacheV1.AddNewCodes(storeId, memberInfoIds, 3);
            DataCacheV1.AddNewCodes(storeId, customerIds, 4);
            DataCacheV1.AddNewCodes(storeId, openIds, 5);
            //Log.Info("set cacheData over");

            //var storeIds = sectoinList.Select(s => s.StoreId).ToList();
            var storeIds = new List<int>();
            if (DataCacheV1.CacheSectionList != null)
                storeIds = DataCacheV1.CacheSectionList.Values.Select(l => l.StoreId).ToList();
            //var storeIds = DataCacheV1.CacheSectionList.Values.Select(l => l.StoreId).ToList();
            var storeExecuteList = pluginExecuteService.GetStoreExecute(storeIds);

            var splitStr = ";";
            var newMemberIds = new List<string>();
            var writeAction = new Dictionary<string, string>();
            var modifyMembers = new List<MemberInfo>();
            var effectList = new List<ActionEffect>();
            var groupPaymentDate = new Dictionary<string, int>();

            Log.Info($"list.count : {list.Count} ,make start ... ");
            foreach (var item in list)
            {
                item.writedate = fileName;
                item.scanorder = 0;
                item.usecoupon = 0;
                item.sectionid = 0;
                item.couponid = "0";
                item.promotionid = "0";
                item.promotionname = "";
                item._paymenttime = item.SetPaymentTime();

                if (!string.IsNullOrEmpty(item.brandcode) && item.brandcode.Length > 2 && !passBrandCodes.Contains(item.brandcode))
                {
                    item.brandcode = item.brandcode.Substring(0, 2);
                }

                if (item.brandcode == "FF" || item.brandcode == "FM")
                    item.brandcode = "15mins";

                item.createtime = DateTime.Now;
                //if (!string.IsNullOrEmpty(item.vipcode))
                //    item.customertags += "A,";
                if (!string.IsNullOrEmpty(item.customerid))
                    item.customertags += "B,";
                if (string.IsNullOrEmpty(item.customertags))
                    item.customertags += "Z,";

                var section = (!string.IsNullOrEmpty(item.sectioncode) && DataCacheV1.CacheSectionBrandList.ContainsKey($"{item.sectioncode}|{item.brandcode}"))
                    ? DataCacheV1.CacheSectionBrandList[$"{item.sectioncode}|{item.brandcode}"] : null;
                if (section == null)
                {
                    errorDic.Add(item.index, $"专柜编码不存在[{item.sectioncode}]");
                    if (item.index > 900000 && !errorDic.Keys.Contains((item.index - 900000) / 20))
                    {
                        errorDic.Add((item.index - 900000) / 20, $"专柜编码不存在[{item.sectioncode}]");
                    }
                    continue;
                }

                item.groupid = section.GroupId;
                item.storeid = section.StoreId;
                item.sectionid = section.SectionId;
                item.brandcode = section.BrandCode;
                storeId = section.StoreId;

                /*
                float allPrice = 0;
                float.TryParse(item.unitprice, out allPrice);
                item.unitprice = item.quantity == 0 ? "0" : Math.Abs(allPrice / item.quantity).ToString();
                */
                var storeExecute = storeExecuteList?.FirstOrDefault(s => s.StoreId == item.storeid);
                var ruleIsLastOrder = storeExecute?.AppConfig?.OrderChangeCustomerRule.IsLastOrder ?? false;
                var ruleIsFirstOrder = storeExecute?.AppConfig?.OrderChangeCustomerRule.IsFirstOrder ?? false;

                if (!groupPaymentDate.ContainsKey($"{item.storeid}|{item._paymenttime.Date}"))
                    groupPaymentDate.Add($"{item.storeid}|{item._paymenttime.Date}", item.storeid);

                var buyerCode = item.buyercode;
                //多个导购编码，取第一个编码找导购
                if (!string.IsNullOrEmpty(buyerCode) && buyerCode.Contains(splitStr))
                    buyerCode = buyerCode.Split(new[] { splitStr }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                //var buyerUserId = buyerList?.FirstOrDefault(b => b.buyerCode == buyerCode)?.buyerUserId ?? 0;
                var buyerUserId = 0;
                if (!string.IsNullOrEmpty(item.buyercode))
                    buyerUserId = DataCacheV1.CacheBuyerList.ContainsKey(item.buyercode) ? DataCacheV1.CacheBuyerList[item.buyercode].BuyerUserId : 0;
                item.buyeruserid = buyerUserId;

                var couponMemberId = "";
                //var userId = 0;
                if (!string.IsNullOrEmpty(item.couponcode))
                {
                    var coupon = couponList.FirstOrDefault(c => c.pwd == item.couponcode);
                    if (coupon != null)
                    {
                        item.couponid = $"{coupon.code}-{coupon.pwd}"; //coupon.code + coupon.pwd;
                        item.promotionid = coupon.actionId;
                        item.promotionname = coupon.actionName;

                        item.submoduleid = coupon.subModuleId;
                        item.submodulename = coupon.subModuleName;

                        //item.memberid = coupon.memberId;
                        couponMemberId = coupon.memberId;

                        if (coupon.moduleId == "3001")
                            item.scanorder = 1;

                        //活动成交顾客次数+1
                        if (!writeAction.Keys.Contains(item.ordercode) && !string.IsNullOrEmpty(coupon.actionId))
                            writeAction.Add(item.ordercode, coupon.actionId);
                    }
                    item.usecoupon = 1;
                }

                var cacheOrderMemberId = DataCacheV1.CacheOrderMemberList.ContainsKey($"{item.storeid}|{item.ordercode}") ? DataCacheV1.CacheOrderMemberList[$"{item.storeid}|{item.ordercode}"] : null;

                //先依据顾客Id查找 MemberInfo，再OpenId,再依据优惠券对应的MemberInfo.Id 查找 (忽略了storeId，不同商户的顾客ID，OpenId应不同)
                var member = (!string.IsNullOrEmpty(item.customerid) && DataCacheV1.CacheBrandMemberIdList.ContainsKey($"{storeId}|{section.BrandCode}|{item.customerid}"))
                    ? DataCacheV1.CacheBrandMemberIdList[$"{storeId}|{section.BrandCode}|{item.customerid}"] :

                    (!string.IsNullOrEmpty(item.openid) && DataCacheV1.CacheMemberOpenIdList.ContainsKey($"{storeId}|{item.openid}"))
                    ? DataCacheV1.CacheMemberOpenIdList[$"{storeId}|{item.openid}"] :

                    (!string.IsNullOrEmpty(couponMemberId) && DataCacheV1.CacheMemberObjectIdList.ContainsKey($"{storeId}|{couponMemberId}"))
                    ? DataCacheV1.CacheMemberObjectIdList[$"{storeId}|{couponMemberId}"] :
                   (!string.IsNullOrEmpty(cacheOrderMemberId) && DataCacheV1.CacheMemberObjectIdList.ContainsKey($"{storeId}|{cacheOrderMemberId}")) ?
                   DataCacheV1.CacheMemberObjectIdList[$"{storeId}|{cacheOrderMemberId}"] : null;

                if (member != null)
                {
                    //会员末单制修改顾客的买手Id - 需判断修改级别
                    if (storeExecute?.AppConfig != null && storeExecute.AppConfig.OrderChangeCustomerRule.IsLastOrder && buyerUserId > 0 && item._paymenttime > member.UpdateTime)
                    {
                        member.BuyerUserId = buyerUserId;
                        member.UpdateTime = item._paymenttime;
                    }
                    //会员首单制修改顾客的买手Id - 需判断修改级别
                    if (storeExecute?.AppConfig != null && storeExecute.AppConfig.OrderChangeCustomerRule.IsFirstOrder && buyerUserId > 0 && item._paymenttime < member.UpdateTime && member.Status == 0)
                    {
                        member.BuyerUserId = buyerUserId;
                        member.UpdateTime = item._paymenttime;
                    }

                    //效果分析
                    //effectList.Add(new ActionEffect(member.Id.ToString(), item._paymenttime));
                    effectList.Add(new ActionEffect(member.Id.ToString(), item.promotionid, item._paymenttime));

                    //Log.Info($"member info:{member.ToJson()}");

                    if (member.SaleData == null)
                        member.SaleData = new SaleDataInfo();
                    if (!member.SaleData.IsBuyer || member.SaleData.LastBuyerTime < item._paymenttime)
                    {
                        member.SaleData.IsBuyer = true;
                        member.SaleData.LastBuyerTime = item._paymenttime;
                    }

                    if (item._birtday != null)
                        member.Birthday = item._birtday;

                    if (member.RegisterTime > item._paymenttime)
                        member.RegisterTime = item._paymenttime;

                    //从‘券验证码’到‘15minsCoupon’到‘MemberInfo’可能会出现此情况
                    if (string.IsNullOrEmpty(member.MemberId) && string.IsNullOrEmpty(item.customerid))
                        member.MemberId = item.customerid;

                    member.GroupId = member.GroupId > 0 ? member.GroupId : item.groupid;
                    member.StoreId = member.StoreId > 0 ? member.StoreId : item.storeid;

                    //member.SectionCode = string.IsNullOrEmpty(item.sectioncode) ? member.SectionCode : item.sectioncode;
                    //member.SectionId = item.sectionid > 0 ? item.sectionid : member.SectionId;

                    member.SectionId = member.SectionId > 0 ? member.SectionId : item.sectionid;
                    member.SectionCode = string.IsNullOrEmpty(member.SectionCode) ? item.sectioncode : member.SectionCode;

                    //modify memberinfo
                    modifyMembers.Add(member);

                    item.userid = member.CustomerId;
                    if (string.IsNullOrEmpty(item.customerid))
                        item.customerid = member.MemberId;

                    //if (string.IsNullOrEmpty(item.memberid))
                    item.memberid = member.Id.ToString();

                    //if (string.IsNullOrEmpty(item.vipcode))
                    //    item.vipcode = member.VipCode;

                    //Log.Info($"item:{item.ToJson()} , member:{member.ToJson()}");

                    if (!string.IsNullOrEmpty(item.customerid))
                        item.vipcode = item.customerid;

                    item.vipleavel = GetCurrentMemberVipLeavel(member, item.vipleavel, item._paymenttime);
                }
                else if (!string.IsNullOrEmpty(item.customerid))
                {
                    /*
                    //有‘顾客ID’但找不到memberInfo ，视为错误数据
                    Log.Info($"connot find member , customerId:{item.customerid}");
                    errorDic.Add(item.index,"找不到顾客信息");
                    continue;*/

                    var newMemBuyerUId = 0;
                    if (storeExecute?.AppConfig != null && (storeExecute.AppConfig.OrderChangeCustomerRule.IsLastOrder ||
                        storeExecute.AppConfig.OrderChangeCustomerRule.IsFirstOrder))
                        newMemBuyerUId = buyerUserId;

                    Log.Info($"new member , customerId:{item.customerid} , vipcode:{item.vipcode} , buyerUserId:{newMemBuyerUId}");

                    item.memberid = new MemberInfo().CreateObjectId().ToString();
                    //写会员数据  [没有openId]
                    var newMember = MDataHelper.CreateMemberByOrder(item, 0, newMemBuyerUId, id: item.memberid);
                    if (newMember != null)
                    {
                        newMember.Status = 0;
                        newMember.UpdateTime = item._paymenttime;
                        //memberList.Add(newMember);
                        //if (DataCacheV1.CacheMemberIdList.ContainsKey($"{storeId}|{item.memberid}"))
                        //    DataCacheV1.CacheMemberIdList.Add($"{storeId}|{item.memberid}", newMember);
                        if (!DataCacheV1.CacheMemberObjectIdList.ContainsKey($"{storeId}|{item.memberid}"))
                            DataCacheV1.CacheMemberObjectIdList.Add($"{storeId}|{item.memberid}", newMember);

                        if (!DataCacheV1.CacheMemberIdList.ContainsKey($"{storeId}|{item.customerid}"))
                            DataCacheV1.CacheMemberIdList.Add($"{storeId}|{item.customerid}", newMember);

                        if (!string.IsNullOrEmpty(item.openid) && !DataCacheV1.CacheMemberOpenIdList.ContainsKey($"{storeId}|{item.openid}"))
                            DataCacheV1.CacheMemberOpenIdList.Add($"{storeId}|{item.openid}", newMember);

                        newMemberIds.Add(item.memberid);
                    }
                    item.vipcode = item.customerid;
                }
                else //if (string.IsNullOrEmpty(item.customerid))
                {
                    item.customerid = item.ordercode;
                }

                //ordercode --> memberid
                if (!string.IsNullOrEmpty(item.memberid) && !DataCacheV1.CacheOrderMemberList.ContainsKey($"{item.storeid}|{item.ordercode}"))
                    DataCacheV1.CacheOrderMemberList.Add($"{item.storeid}|{item.ordercode}", item.memberid);
            };

            noSectionCodes = list.Where(l => l.sectionid < 1).Select(l => l.sectioncode).ToList();
            list = list.Where(l => l.sectionid > 0).ToList();
            pluginDataService.AddPluginOrderInfoList<PluginOrderTxt>(list);
            Log.Info($"DataService.AddPluginOrderInfoList.count : {list.Count} ,  list.ToJson()");

            //修改memberInfo
            if (modifyMembers.Any())
                MDataHelper.ModifyMemberByOrder(modifyMembers);

            //写活动参与信息
            if (effectList.Any())
            {
                actionService.WriteMemberBack(effectList);
                Log.Info($"活动反馈数：{effectList.Count} ==> effectList.ToJson()");
            }

            //写活动参与人数
            if (writeAction.Any())
            {

                //写活动执行效果
                var salist = writeAction.ToList().GroupBy(g => g.Value).Select(l => new
                {
                    actionId = l.Key,
                    addCount = l.Count()
                }).ToList();
                var dic = new Dictionary<string, int>();
                salist.ForEach(l =>
                {
                    if (!dic.Keys.Contains(l.actionId))
                        dic.Add(l.actionId, l.addCount);
                });
                actionService.WriteConversionRate(dic);
                Log.Info($"参与活动信息：write action :{dic.Count} dic.ToJson()");
            }

            //写订单支付日期组
            if (groupPaymentDate.Any())
            {
                var logList = groupPaymentDate.ToList().Select(g => new StoreDataPressLog
                {
                    StoreId = g.Value,
                    Time = DateTime.Parse(g.Key.Split('|')[1]),
                    Type = StoreDataPressType.Order,
                    Status = StoreDataPressStatus.UnDeal,
                    CreateDate = DateTime.Now
                }).ToList();
                storeDataAnchorService.AddStoreDataPressLogList(logList);
                Log.Info($"写订单支付日期组：write logList :{logList.Count}, logList.ToJson()");
            }

            //写 特权优惠券为已使用
            if (couponCodes.Any())
            {
                ModifyCouponStatus(couponCodes);
                Log.Info($"修改特权优惠券为已使用 :{couponCodes.Count} couponCodes.ToJson()");
            }
            return errorDic;




            return null;
        }



        


        public static List<string> SplitBuyerCode(List<string> list)
        {
            var splitStr = ";";
            var codeQuery = list.Where(code => !string.IsNullOrEmpty(code) && code.Contains(splitStr));
            if (codeQuery != null && codeQuery.Any())
            {
                var splitCodes = codeQuery.Distinct().ToList();
                splitCodes.ForEach(code =>
                {
                    list.Add(code.Split(new[] { splitStr }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault());
                });
            }
            return list.Distinct().ToList();
        }


        #region  --  拆分优惠券  --


        public static List<PluginOrderTxt> SplitCoupon(List<PluginOrderTxt> list)
        {
            //以替换后的 cn逗号 拆分多个优惠券 ， 改：以分号拆分多个优惠券
            var splitStr = ";";
            //var orderCodeList = list.Where(l => l.couponcode.Contains(splitStr)).Select(o => o.ordercode).Distinct().ToList();
            var orderCodeList = new List<string>();

            var orderCodeQuery = list.Where(l => !string.IsNullOrEmpty(l.couponcode) && l.couponcode.Contains(splitStr) && l.quantity > 0).Select(o => o.ordercode);
            if (orderCodeQuery.Any())
                orderCodeList = orderCodeQuery.Distinct().ToList();
            else return list;

            if (orderCodeList.Count < 1)
                return list;

            //var moreCouponLst = list.Where(l => orderCodeList.Contains(l.ordercode)).ToList();
            var removeIndex = new List<int>();
            var newIndex = 1f;

            orderCodeList.ForEach(code =>
            {
                Log.Info($"==> orderCode : {code}");
                //var orderItams = list.Where(l => l.ordercode == code && !string.IsNullOrEmpty(l.couponcode)).ToList();
                var orderItams = list.Where(l => l.ordercode == code && l.quantity > 0).ToList();
                var mCouponArray = new List<string>();
                var sCouponArray = new List<string>();
                var mProductCount = 0f;
                var sProductCount = 0f;

                orderItams.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.couponcode) && item.couponcode.Contains(splitStr))
                    {
                        mCouponArray.AddRange(item.couponcode.Split(new[] { splitStr }, StringSplitOptions.RemoveEmptyEntries).ToList());
                        mProductCount += item.quantity;
                    }
                    else if (!string.IsNullOrEmpty(item.couponcode))
                    {
                        sCouponArray.Add(item.couponcode);
                        sProductCount += item.quantity;
                    }
                });

                mCouponArray = mCouponArray.Distinct().ToList();
                sCouponArray = sCouponArray.Distinct().ToList();

                var tCouponArray = mCouponArray;
                tCouponArray.AddRange(sCouponArray);
                tCouponArray = tCouponArray.Distinct().ToList();

                var splitItems = new List<PluginOrderTxt>();
                var splitCoupons = new List<string>();

                //多个优惠券订单项 产品总数量不少于优惠券总数量 ，可直接对多个券的数据行进行拆分
                if (mCouponArray.Count <= mProductCount)
                {
                    Log.Info($"  -- SplitCoupon by A, orderCode:{code}, couponCount:{mCouponArray.Count}, productCount:{mProductCount}");
                    splitItems = orderItams.Where(l => !string.IsNullOrEmpty(l.couponcode) && l.couponcode.Contains(splitStr)).ToList();
                    splitCoupons = mCouponArray;
                }
                //所有含优惠券订单项 产品总数量不少于优惠券总数量 ，可只对使用优惠券数据行进行拆分
                else if (tCouponArray.Count <= mProductCount + sProductCount)
                {
                    Log.Info($"  -- SplitCoupon by B, orderCode:{code}, couponCount:{tCouponArray.Count}, productCount:{mProductCount + sProductCount}");
                    splitItems = orderItams.Where(l => !string.IsNullOrEmpty(l.couponcode)).ToList();
                    splitCoupons = tCouponArray;
                }
                //订单所有数据行进行拆分
                else
                {
                    Log.Info($"  -- SplitCoupon by C, orderCode:{code}, couponCount:{tCouponArray.Count}, productCount:{mProductCount + sProductCount}");

                    var productCount = orderItams.Sum(i => i.quantity);
                    if (productCount < tCouponArray.Count)
                        Log.Info($"  -- orderCode:{code}, 产品总数{productCount}, 使用优惠券{tCouponArray.Count}个,出现拆分丢失优惠券{tCouponArray.Count - productCount}个；{tCouponArray.ToJson()}");

                    splitItems = orderItams;
                    splitCoupons = tCouponArray;
                }

                //Log.Info($"splitItems.count :{splitItems.Count}");

                splitItems.ForEach(item =>
                {
                    Log.Info($"  -- splitItems: item.index = {item.index}, item.quantity = {item.quantity}");

                    removeIndex.Add(item.index);
                    var newItms = SplitOrderItem(item, splitCoupons, (int)newIndex);
                    list.AddRange(newItms);
                    var removeCount = (int)item.quantity > splitCoupons.Count ? splitCoupons.Count : (int)item.quantity;
                    if (removeCount < 0) removeCount = 0;
                    splitCoupons.RemoveRange(0, removeCount);

                    newIndex += item.quantity;
                });
            });

            Log.Info($"removeIndex : {removeIndex.Count}, {removeIndex.ToJson()}");
            return list.Where(l => !removeIndex.Contains(l.index)).ToList();
        }


        private static List<PluginOrderTxt> SplitOrderItem(PluginOrderTxt order, List<string> coupons, int index)
        {
            double avgP = order._paymentprice / order.quantity;
            var newItems = new List<PluginOrderTxt>();

            for (var i = 0; i < order.quantity; i++)
            {
                var item = (PluginOrderTxt)order.Clone();

                item.couponcode = coupons.Count > i ? coupons[i] : "";
                //修改支付时间，避免md5key重复
                item.paymenttime = order._paymenttime.AddSeconds(i + 1).ToString("yyyy-MM-dd HH:mm:ss");
                item.index = 900000 + order.index * 20 + i;
                item._result.md5key = EncryptHelper.MD5Encrypt($"{order.ordercode}{order.sku}{order.productcode}{order.color}{order.size}{order.paymenttime}");
                item.quantity = 1;

                if (i == 0)
                {
                    item._paymentprice = order._paymentprice - avgP * (order.quantity - 1);
                }
                else
                {
                    item._paymentprice = avgP;
                }

                newItems.Add(item);
                Log.Info($"  -- splitItem : new item index = {item.index} , coupon = {item.couponcode}, md5key = {item._result.md5key}");
            }
            return newItems;
        }


        #endregion
    }
}