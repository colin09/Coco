using System;
using System.Linq;
using System.Collections.Generic;

using com.mh.model.enums;
using com.mh.model.mongo.dbMh;
using com.mh.common.Logger;
using com.mh.common.ioc;
using com.mh.mongo.iservice;
using com.mh.model.mongo.mgBase;
using com.mh.model.messages;
using com.mh.rabbit;
using com.mh.model.messages.message;

namespace com.mh.dataengine.common.helper
{
    public class MDataHelper
    {
        private static ILog log => IocProvider.GetService<ILog>();
        private static IPluginDataService pluginDataService => IocProvider.GetService<IPluginDataService>();



        /*
        /// <summary>
        /// 过滤重复数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="brandCode"></param>
        /// <returns></returns>
        public static Dictionary<int, string> Filter<T>(List<T> list, int storeId) where T : PluginOrderBase
        {
            log.Info("filter start ...");
            var dic = new Dictionary<int, string>();
            var startTime = list.Min(l => l._paymenttime).Date;
            var endTime = list.Max(l => l._paymenttime).Date.AddDays(1);

            log.Info($"start read filter area, from {startTime} to {endTime} ...");
            var filterList = pluginDataService.GetPluginOrderList<T>(startTime, endTime, storeId);
            if (filterList == null)
                return dic;
            log.Info($"get filter list ,count:{filterList.Count()} ...");

            var listDic = list.ToDictionary(l => l.index, l => l._result.md5key);
            var errorlist = listDic.Where(kv => filterList.Contains(kv.Value)).ToList();

            log.Info($"get filter error list ,count:{errorlist.Count()} ...");

            if (errorlist.Any())
                errorlist.ForEach(l => dic.Add(l.Key, "重复数据"));
            return dic;
        }*/


        public static Dictionary<int, string> FilterByKey<T>(List<T> list, int storeId) where T : PluginOrderBase
        {
            log.Info("filter start (byKey) ...");
            var dic = new Dictionary<int, string>();
            var keys = list.Select(l => l._result.md5key).ToList();

            var filterList = pluginDataService.GetPluginOrderList<T>(keys, storeId);
            if (filterList == null)
                return dic;
            log.Info($"get filter list ,count:{filterList.Count()} ...");

            var listDic = list.ToDictionary(l => l.index, l => l._result.md5key);
            var errorlist = listDic.Where(kv => filterList.Contains(kv.Value)).ToList();

            log.Info($"get filter error list ,count:{errorlist.Count()} ...");

            if (errorlist.Any())
                errorlist.ForEach(l => dic.Add(l.Key, "重复数据"));
            return dic;
        }


        public static Dictionary<int, string> Filter<T>(List<T> list) where T : PluginOrderBase
        {
            var dic = new Dictionary<int, string>();
            var keys = list.Select(l => l._result.md5key).ToList();

            log.Info("filter start ...");
            var filterList = pluginDataService.GetPluginOrderList<T>(keys);
            if (filterList == null)
                return dic;
            log.Info($"get filter list ,count:{filterList.Count()} ...");

            var listDic = list.ToDictionary(l => l.index, l => l._result.md5key);
            var errorlist = listDic.Where(kv => filterList.Contains(kv.Value)).ToList();

            log.Info($"get filter error list ,count:{errorlist.Count()} ...");

            if (errorlist.Any())
                errorlist.ForEach(l => dic.Add(l.Key, "重复数据"));
            return dic;
        }





        public static MemberInfo CreateMemberByOrder<T>(T item, int userId, int buyerUserId, string memberId = "", string id = "") where T : PluginOrderBase
        {
            var newMem = new MemberInfo()
            {
                GroupId = item.groupid,
                StoreId = item.storeid,
                SectionId = item.sectionid,
                //BuyerUserId = item.buyeruserid,
                BuyerUserId = buyerUserId,
                SectionCode = item.sectioncode,
                VipCode = item.vipcode,
                CustomerId = userId,
                MemberId = string.IsNullOrEmpty(memberId) ? item.customerid : memberId,
                Birthday = item._birtday,
                MobilePhone = "",
                Email = "",
                Level = 0,
                OutSite = new OutSiteInfo()
                {
                    OutSiteType = (int)OutsiteType.None,
                    From = "order",
                    OutSiteUId = item.openid,
                    Detail = new Dictionary<string, string>(),
                },
                SubScribe = new SubScribeInfo() { IsSubscribe = true, IsScan = true },
                SaleData = new SaleDataInfo() { IsBuyer = true, LastBuyerTime = item._paymenttime },
                Status = (int)DataStatus.Normal,
                //RegisterTime = item._paymenttime,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                CreateUser = 0,
                UpdateUser = 0
            };

            newMem.OutSite.Detail.Add("BrandCode", item.brandcode);
            newMem.OutSite.Detail.Add("StoreNo", item.sectioncode);

            if (!string.IsNullOrEmpty(id))
            {
                newMem.Id = MagicHorseBase.GetObjectId(id);
            }

            //由mq执行添加
            //mongoDB 写 order 有延时，弃用此方法
            MessagelistenerClient.SyncMemberInfo(MemberInfoSyncType.Add, new MemberInfoDto()
            {
                Id = newMem.StringId,
                GroupId = newMem.GroupId,
                StoreId = newMem.StoreId,
                SectionId = newMem.SectionId,
                BuyerUserId = newMem.BuyerUserId,
                SectionCode = newMem.SectionCode,
                VipCode = newMem.VipCode,
                UserId = newMem.CustomerId,
                CustomerId = newMem.MemberId,
                Birtday = newMem.Birthday,
                MobilePhone = newMem.MobilePhone,

                OutsiteType = (int)OutsiteType.None,
                BrandCode = item.brandcode,
                Paymenttime = item._paymenttime,
                IsBuy = true
            });

            //DataService.AddMemberInfo(newMem);
            /*
            //延迟同步到ES
            DataService.AddMemberInfoList(new List<string>()
            {
                newMem.StringId
            });*/

            return newMem;
        }


        /*
        // 转移到mq
        public static void CreateMemberManger(MemberInfo member)
        {
            var memManager = new MemberInfoManager()
            {
                MemberId = member.StringId,
                BuyerUserId = member.BuyerUserId,
                ManagerLevel = 2,
                Status = MemberManagerState.Normal,
                LastContactTime = DateTime.MinValue,
                VipCode = member.VipCode,
                CreateDate = DateTime.Now,
                CreateUser = 0,
                UpdateDate = DateTime.Now,
                UpdateUser = 0,
                UpdateLog = new List<MemberManagerLog>()
                {
                    new MemberManagerLog()
                    {
                        MemberId = member.StringId,BuyerUserId = member.BuyerUserId,
                        ManagerLevel = 2,UpdateDate =DateTime.Now,UpdateUser = 0,
                    }
                }
            };

            //insert
            pluginDataService.AddMemberManager(memManager);
        }*/



        public static void ModifyMemberByOrder(List<MemberInfo> list)
        {
            if (list == null || list.Count < 1)
                return;
            var tempList = new List<SyncMemberInfoTemp>();
            list.ForEach(mem =>
            {
                //由mq执行修改
                MessagelistenerClient.SyncMemberInfo(MemberInfoSyncType.ModifyByOrder, new MemberInfoDto()
                {
                    Id = mem.StringId,
                    GroupId = mem.GroupId,
                    StoreId = mem.StoreId,
                    BuyerUserId = mem.BuyerUserId,
                    UserId = mem.CustomerId,


                    SectionId = mem.SectionId,
                    SectionCode = mem.SectionCode,

                    CustomerId = mem.MemberId,
                    VipCode = mem.VipCode,
                    Birtday = mem.Birthday,

                    Paymenttime = mem.SaleData.LastBuyerTime,
                    IsBuy = true
                });
                tempList.Add(new SyncMemberInfoTemp() { MemberId = mem.StringId, CreateTime = DateTime.Now.AddMinutes(30), Status = DataStatus.Normal });
                //tempList.Add(mem.StringId);
            });

            //延迟同步到ES (订单统计相关数据)
            if (tempList.Count > 0)
                pluginDataService.AddMemberInfoList(tempList);
            //DataService.AddSyncMemberTemp(tempList);
        }







    }
}
