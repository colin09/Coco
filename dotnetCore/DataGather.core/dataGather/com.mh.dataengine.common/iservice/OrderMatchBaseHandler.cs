using System;
using System.Linq;
using MongoDB.Bson;
using System.Collections.Generic;

using com.mh.common.ioc;
using com.mh.common.Logger;
using com.mh.model.mongo.dbMh;
using com.mh.model.mysql.entity;
using com.mh.mysql.factory;
using com.mh.mysql.repository.dbSession;
using com.mh.mongo.iservice;


namespace com.mh.dataengine.common.iservice
{
    public class OrderMatchBaseHandler
    {
        protected static ILog Log => IocProvider.GetService<ILog>();
        protected static IPluginDataService pluginDataService => IocProvider.GetService<IPluginDataService>();
        protected static IStrategyActionService actionService => IocProvider.GetService<IStrategyActionService>();
        protected static IPluginExecuteService pluginExecuteService => IocProvider.GetService<IPluginExecuteService>();
        protected static IStoreDataAnchorService storeDataAnchorService => IocProvider.GetService<IStoreDataAnchorService>();
        protected static IStorePreferenceService storePreferenceService => IocProvider.GetService<IStorePreferenceService>();
        protected static ICouponService couponService => IocProvider.GetService<ICouponService>();
        protected static IPrivilegeService privilegeService => IocProvider.GetService<IPrivilegeService>();
        //protected static IMemberInfoService _memberinfoService => IocProvider.GetService<IMemberInfoService>();


        protected static List<FfCouponTem> GetFfCouponList(int storeId, List<string> couponCodes)
        {
            if (!couponCodes.Any())
                return new List<FfCouponTem>();
            // var db = new MagicalHorseContext("MagicalHorseContext");
            // var query = db.Set<Fifteen_Mins_CouponEntity>().Where(c => /*c.StoreId == storeId &&*/ couponCodes.Contains(c.Pwd));

            var session = DbSessionFactory.GetCurrentDbSession("MagicalHorse") as MagicHorseSession;
            var query = session.FifMinsCouponRepository.Where(c => couponCodes.Contains(c.Pwd));

            if (storeId > 0)
                query = query.Where(c => c.StoreId == storeId);
            var couponList = query
                .Select(coupon => new FfCouponTem
                {
                    code = coupon.Code,
                    pwd = coupon.Pwd,
                    userId = coupon.UserId,
                    memberId = coupon.MemberId,

                    moduleId = coupon.ModuleId.ToString(),
                    moduleName = coupon.ModuleName,

                    subModuleId = coupon.SubModuleId,
                    subModuleName = coupon.SubModuleName,

                    actionId = coupon.StrategyActionId,
                    actionName = coupon.StrategyActionName

                }).ToList();
            return couponList;
        }

        protected static List<FfCouponTem> GetPrivilegeCouponList(int storeId, List<string> couponCodes)
        {
            if (!couponCodes.Any())
                return new List<FfCouponTem>();
            // var db = new MagicalHorseContext("MagicalHorseContext");
            // var query = db.Set<Fifteen_Mins_Privilege_CouponEntity>().Where(c => /*c.StoreId == storeId &&*/ couponCodes.Contains(c.Pwd));

            var session = DbSessionFactory.GetCurrentDbSession("MagicalHorse") as MagicHorseSession;
            var query = session.FifMinsPrivilegeCouponRepository.Where(c => couponCodes.Contains(c.Pwd));

            if (storeId > 0)
                query = query.Where(c => c.StoreId == storeId);
            var couponList = query
                .Select(coupon => new FfCouponTem
                {
                    code = coupon.Code,
                    pwd = coupon.Pwd,
                    userId = coupon.UserId,
                    memberId = coupon.MemberId,

                    moduleId = coupon.ModuleId.ToString(),
                    moduleName = coupon.ModuleName,

                    subModuleId = coupon.SubModuleId.ToString(),
                    subModuleName = coupon.SubModuleName,

                    actionId = coupon.StrategyActionId,
                    actionName = coupon.StrategyActionName

                }).ToList();
            return couponList;
        }

        protected static List<FfCouponTem> GetMgCouponList(int storeId, List<string> couponCodes)
        {
            if (couponCodes == null || couponCodes.Count < 1)
                return new List<FfCouponTem>();
            var list = couponService.GetCouponList(storeId, couponCodes);

            if (list == null || list.Count() < 1)
                return new List<FfCouponTem>();
            Log.Info($"GetMgCouponList : {list.ToJson()}");
            var couponList = list.Select(coupon => new FfCouponTem
            {
                code = coupon.Code,
                pwd = coupon.Pwd,
                //userId = coupon.UserId,
                memberId = coupon.MemberId,

                moduleId = coupon.ModuleId.ToString(),
                moduleName = coupon.ModuleName,

                subModuleId = coupon.SubModuleId,
                subModuleName = coupon.SubModuleName,

                actionId = coupon.StrategyActionId,
                actionName = coupon.StrategyActionName

            }).ToList();
            return couponList;
        }


        protected static List<FfCouponTem> GetAllCouponList(int storeId, List<string> couponCodes)
        {
            var ffCouponList = GetFfCouponList(storeId, couponCodes);
            var mgCouponList = GetMgCouponList(storeId, couponCodes);
            var privilegeCouponList = GetPrivilegeCouponList(storeId, couponCodes);

            var list = new List<FfCouponTem>();
            if (ffCouponList != null && ffCouponList.Count > 0)
                list.AddRange(ffCouponList);
            if (mgCouponList != null && mgCouponList.Count > 0)
                list.AddRange(mgCouponList);
            if (privilegeCouponList != null && privilegeCouponList.Count > 0)
                list.AddRange(privilegeCouponList);

            return list;
        }



        protected static string GetCurrentMemberVipLeavel(MemberInfo member, string vipLeavel, DateTime payTime)
        {
            var leavel = vipLeavel;
            if (string.IsNullOrEmpty(vipLeavel) && member.OutSite != null && member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                var registerTime = DateTime.MinValue;
                if (member.OutSite.Detail.ContainsKey("OriginalRegisterTime"))
                {
                    var str = member.OutSite.Detail["OriginalRegisterTime"];
                    if (DateTime.TryParse(str, out registerTime))
                    {
                        if (registerTime <= payTime)
                        {
                            var vipName = storePreferenceService.GetVipLevelName(member.StoreId,
                                member.OutSite.Detail["CardLevel"]);
                            leavel = !string.IsNullOrWhiteSpace(vipName) ? vipName : member.OutSite.Detail["CardLevel"];
                        }
                        else
                        {
                            leavel = "非会员";
                        }
                    }
                    else
                    {
                        var vipName = storePreferenceService.GetVipLevelName(member.StoreId, member.OutSite.Detail["CardLevel"]);
                        leavel = !string.IsNullOrWhiteSpace(vipName) ? vipName : member.OutSite.Detail["CardLevel"];
                    }
                }
                else
                {
                    leavel = "非会员";
                }
            }
            else if (member.OutSite?.Detail == null || !member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                leavel = "非会员";
            }

            return leavel;
        }


        protected static string GetMemberVipLeavel(MemberInfo member, string vipLeavel, DateTime payTime)
        {


            var leavel = vipLeavel;
            if (string.IsNullOrEmpty(vipLeavel) && member.OutSite != null && member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                var registerTime = DateTime.MinValue;
                if (member.OutSite.Detail.ContainsKey("OriginalRegisterTime"))
                {
                    var str = member.OutSite.Detail["OriginalRegisterTime"];
                    if (DateTime.TryParse(str, out registerTime))
                    {
                        if (registerTime <= payTime)
                        {
                            var vipName = storePreferenceService.GetVipLevelName(member.StoreId,
                                member.OutSite.Detail["CardLevel"]);
                            leavel = !string.IsNullOrWhiteSpace(vipName) ? vipName : member.OutSite.Detail["CardLevel"];
                        }
                        else
                        {
                            leavel = "非会员";
                        }
                    }
                    else
                    {
                        var vipName = storePreferenceService.GetVipLevelName(member.StoreId, member.OutSite.Detail["CardLevel"]);
                        leavel = !string.IsNullOrWhiteSpace(vipName) ? vipName : member.OutSite.Detail["CardLevel"];
                    }
                }
                else
                {
                    leavel = "非会员";
                }
            }
            else if (member.OutSite?.Detail == null || !member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                leavel = "非会员";
            }

            return leavel;
        }

        protected static string GetEvaMemberVipLevel(MemberInfo member, string vipLeavel, DateTime payTime)
        {
            var strLevel = "非会员";
            if (!string.IsNullOrEmpty(vipLeavel.Trim()))
            {
                int level = -888;
                Int32.TryParse(vipLeavel, out level);
                switch (level)
                {
                    case 1: strLevel = "普卡"; break;
                    case 2: strLevel = "金卡"; break;
                    case 3: strLevel = "白金卡"; break;
                    case 4: strLevel = "钻石卡"; break;
                    case 6: strLevel = "7折白金卡"; break;
                    case 7: strLevel = "公司赠卡"; break;
                    case 8: strLevel = "无卡类型(丽晶中无卡类型)"; break;
                    case 9: strLevel = "不积分卡"; break;
                    case 10: strLevel = "五折储值卡"; break;
                    case 11: strLevel = "特卖场VIP"; break;
                    case 12: strLevel = "银卡"; break;
                    case -888: case 0: strLevel = "非会员"; break;
                }
            }
            else if (member?.OutSite != null && member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                var registerTime = DateTime.MinValue;
                if (member.OutSite.Detail.ContainsKey("OriginalRegisterTime"))
                {
                    var str = member.OutSite.Detail["OriginalRegisterTime"];
                    if (DateTime.TryParse(str, out registerTime))
                    {
                        if (registerTime <= payTime)
                        {
                            var vipName = storePreferenceService.GetVipLevelName(member.StoreId, member.OutSite.Detail["CardLevel"]);
                            strLevel = !string.IsNullOrWhiteSpace(vipName) ? vipName : member.OutSite.Detail["CardLevel"];
                        }
                    }
                }

            }
            return strLevel;
        }


        protected static void ModifyCouponStatus(List<string> codes)
        {
            // var db = new MagicalHorseContext("MagicalHorseContext");
            // var fquery = db.Set<Fifteen_Mins_CouponEntity>().Where(c => codes.Contains(c.Pwd) && (c.Status != FifteenMinCouponType.Verify));

            var session = DbSessionFactory.GetCurrentDbSession("MagicalHorse") as MagicHorseSession;
            var fquery = session.FifMinsCouponRepository.Where(c => codes.Contains(c.Pwd) && c.CouponStatus != FifteenMinCouponType.Verify);

            if (fquery.Any())
            {
                fquery.ToList().ForEach(coupon =>
                {
                    coupon.CouponStatus = FifteenMinCouponType.Verify;

                    // ? session.FifMinsCouponRepository.Update(coupon);
                });
            }
            //var pquery = db.Set<Fifteen_Mins_Privilege_CouponEntity>().Where(c => codes.Contains(c.Pwd) && (c.Status != FifteenMinCouponType.Verify));
            var pquery = session.FifMinsPrivilegeCouponRepository.Where(c => codes.Contains(c.Pwd) && c.CouponStatus != FifteenMinCouponType.Verify);
            if (pquery.Any())
            {
                pquery.ToList().ForEach(coupon =>
                {
                    coupon.CouponStatus = FifteenMinCouponType.Verify;

                    // ? session.FifMinsPrivilegeCouponRepository.Update(coupon);
                });
            }

            session.SaveChange();

            privilegeService.UsePrivilegeCoupon(codes);
            couponService.UseCouponInfo(codes);
        }

    }




    public class FfCouponTem
    {
        public string code { set; get; }
        public string pwd { set; get; }
        public int userId { set; get; }
        public string memberId { set; get; }

        public string moduleId { set; get; }
        public string moduleName { set; get; }

        public string subModuleId { set; get; }
        public string subModuleName { set; get; }

        public string actionId { set; get; }
        public string actionName { set; get; }

    }
}
