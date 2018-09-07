using System;
using System.Linq;
using System.Collections.Generic;

using com.mh.common.ioc;
using com.mh.common.Logger;
using com.mh.common.extension;
using com.mh.mongo.iservice;
using com.mh.model.enums;
using com.mh.model.mongo.dbMh;
using com.mh.model.mysql.entity;
using com.mh.mysql.factory;
using com.mh.mysql.repository.dbSession;

namespace com.mh.dataengine.common.cache
{
    public class DataCacheV1
    {

        protected static ILog log => IocProvider.GetService<ILog>();
        protected static IPluginDataService pluginDataService => IocProvider.GetService<IPluginDataService>();



        /// <summary>
        /// sectionCode ---- SectionTem
        /// </summary>
        public static Dictionary<string, SectionTem> CacheSectionList = new Dictionary<string, SectionTem>();
        public static Dictionary<string, SectionTem> CacheSectionBrandList = new Dictionary<string, SectionTem>();

        /// <summary>
        /// buyerCode ---- BuyerTem
        /// </summary>
        public static Dictionary<string, BuyerTem> CacheBuyerList = new Dictionary<string, BuyerTem>();


        /// <summary>
        /// memberId ---- MemberInfo.id
        /// memberId ---- MemberInfo.memberId
        /// openId ---- MemberInfo
        /// vipcode ---- MemberInfo
        /// userid ---- MemberInfo
        /// </summary>
        public static Dictionary<string, MemberInfo> CacheMemberObjectIdList = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, MemberInfo> CacheMemberIdList = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, MemberInfo> CacheMemberOpenIdList = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, MemberInfo> CacheMemberVipCodeList = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, MemberInfo> CacheMemberUserIdList = new Dictionary<string, MemberInfo>();

        public static Dictionary<string, MemberInfo> CacheMemberLoginNameList = new Dictionary<string, MemberInfo>();


        public static Dictionary<string, MemberInfo> CacheBrandMemberIdList = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, MemberInfo> CacheBrandMemberVipCodeList = new Dictionary<string, MemberInfo>();
        public static Dictionary<string, MemberInfo> CacheBrandMemberUserIdList = new Dictionary<string, MemberInfo>();

        public static Dictionary<string, string> CacheVipLevelList = new Dictionary<string, string>();

        public static Dictionary<string, string> CacheOrderMemberList = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="codes"></param>
        /// <param name="key">1-sectionCode，2-buyerCode，3-member.ids，4-memberIds，5-openIds，6-vipCodes，7-userIds</param>
        public static void AddNewCodes(int storeId, List<string> codes, int key)
        {
            var newCodes = new List<string>();
            switch (key)
            {
                case 1: //sectionCode
                    //newCodes = codes.Where(code => !CacheSectionList.ContainsKey(code)).ToList(); //更新不及时
                    newCodes = codes;
                    if (newCodes.Any() && newCodes.Count > 0) UpdateSectionList(storeId, newCodes);
                    break;
                case 2: //buyerCode
                    //newCodes = codes.Where(code => !CacheBuyerList.ContainsKey(code)).ToList(); //更新不及时
                    newCodes = codes;
                    if (newCodes.Any() && newCodes.Count > 0) UpdateBuyerList(storeId, newCodes);
                    break;

                case 3: //member.ids
                    newCodes = codes.Where(code => !CacheMemberObjectIdList.ContainsKey(code)).ToList();
                    if (newCodes.Any() && newCodes.Count > 0) UpdateMemberList(storeId, key, ids: newCodes);
                    break;
                case 4: //member.memberIds
                    newCodes = codes.Where(code => !CacheMemberIdList.ContainsKey(code)).ToList();
                    if (newCodes.Any() && newCodes.Count > 0) UpdateMemberList(storeId, key, memberIds: newCodes);
                    break;
                case 5: //member.openIds
                    newCodes = codes.Where(code => !CacheMemberOpenIdList.ContainsKey(code)).ToList();
                    if (newCodes.Any() && newCodes.Count > 0) UpdateMemberList(storeId, key, openIds: newCodes);
                    break;
                case 6: //member.vipCodes
                    newCodes = codes.Where(code => !CacheMemberVipCodeList.ContainsKey(code)).ToList();
                    if (newCodes.Any() && newCodes.Count > 0) UpdateMemberList(storeId, key, vipCodes: newCodes);
                    break;
                case 7: //member.userIds
                    var userIds = codes.Where(code => !CacheMemberUserIdList.ContainsKey(code)).Select(Int32.Parse).ToList();
                    if (newCodes.Any() && newCodes.Count > 0) UpdateMemberList(storeId, key, userIds: userIds);
                    break;
                case 8: //member.loginname -> 133 baroque
                    var newNames = codes.Where(code => !CacheMemberLoginNameList.ContainsKey(code)).ToList();
                    if (newNames.Any() && newNames.Count > 0) UpdateMemberList(storeId, key, lNames: newNames);
                    break;
            }
        }

        private static void UpdateMemberList(int storeId, int key, List<string> memberIds = null, List<string> openIds = null, List<string> ids = null, List<string> vipCodes = null, List<int> userIds = null, List<string> lNames = null)
        {
            var list = pluginDataService.GetMemberList(storeId, ids: ids, memberIds: memberIds, openIds: openIds, vipCodes: vipCodes, userIds: userIds, loginNames: lNames);
            if (list != null)
            {
                switch (key)
                {
                    case 3: //member.ids
                        //var dic3 = list.ToDictionary(k => k.StringId, val => val);
                        list.ForEach(item =>
                        {
                            if (!CacheMemberObjectIdList.ContainsKey($"{item.StoreId}|{item.StringId}"))
                                CacheMemberObjectIdList.Add($"{item.StoreId}|{item.StringId}", item);
                        });
                        break;
                    case 4: //member.memberIds
                        list.ForEach(item =>
                        {
                            if (!CacheMemberIdList.ContainsKey($"{item.StoreId}|{item.MemberId}"))
                                CacheMemberIdList.Add($"{item.StoreId}|{item.MemberId}", item);

                            //if (!CacheSectionMemberIdList.ContainsKey($"{storeId}|{item.SectionId}|{item.MemberId}"))
                            //    CacheSectionMemberIdList.Add($"{storeId}|{item.SectionId}|{item.MemberId}", item);

                            if (item.OutSite?.Detail?.ContainsKey("BrandCode") == true)
                                if (!CacheBrandMemberIdList.ContainsKey($"{item.StoreId}|{item.OutSite.Detail["BrandCode"]}|{item.MemberId}"))
                                    CacheBrandMemberIdList.Add($"{item.StoreId}|{item.OutSite.Detail["BrandCode"]}|{item.MemberId}", item);
                        });
                        break;
                    case 5: //member.openIds
                        list.ForEach(item =>
                        {
                            if (!CacheMemberOpenIdList.ContainsKey($"{item.StoreId}|{item.OutSite.OutSiteUId}"))
                                CacheMemberOpenIdList.Add($"{item.StoreId}|{item.OutSite.OutSiteUId}", item);
                        });
                        break;
                    case 6: //member.vipCodes
                        list.ForEach(item =>
                        {
                            if (!CacheMemberVipCodeList.ContainsKey($"{item.StoreId}|{item.VipCode}"))
                                CacheMemberVipCodeList.Add($"{item.StoreId}|{item.VipCode}", item);

                            //if (!CacheSectionMemberVipCodeList.ContainsKey($"{storeId}|{item.SectionId}|{item.MemberId}"))
                            //    CacheSectionMemberVipCodeList.Add($"{storeId}|{item.SectionId}|{item.MemberId}", item);

                            if (item.OutSite?.Detail?.ContainsKey("BrandCode") == true)
                                if (!CacheBrandMemberVipCodeList.ContainsKey($"{item.StoreId}|{item.OutSite.Detail["BrandCode"]}|{item.MemberId}"))
                                    CacheBrandMemberVipCodeList.Add($"{item.StoreId}|{item.OutSite.Detail["BrandCode"]}|{item.MemberId}", item);
                        });
                        break;
                    case 7: //member.userIds int
                        list.ForEach(item =>
                        {
                            if (!CacheMemberUserIdList.ContainsKey($"{item.StoreId}|{item.CustomerId.ToString()}"))
                                CacheMemberUserIdList.Add($"{item.StoreId}|{item.CustomerId.ToString()}", item);

                            //if (!CacheSectionMemberUserIdList.ContainsKey($"{storeId}|{item.SectionId}|{item.CustomerId.ToString()}"))
                            //    CacheSectionMemberUserIdList.Add($"{storeId}|{item.SectionId}|{item.CustomerId.ToString()}", item);

                            if (item.OutSite?.Detail?.ContainsKey("BrandCode") == true)
                                if (!CacheBrandMemberUserIdList.ContainsKey($"{item.StoreId}|{item.OutSite.Detail["BrandCode"]}|{item.CustomerId.ToString()}"))
                                    CacheBrandMemberUserIdList.Add($"{item.StoreId}|{item.OutSite.Detail["BrandCode"]}|{item.CustomerId.ToString()}", item);
                        });
                        break;
                    case 8: //member.outsite.loginname -> 133 baroque
                        list.ForEach(item =>
                        {
                            //if (!CacheMemberLoginNameList.ContainsKey($"{storeId}|{item.SectionId}|{item.OutSite.LoginName}"))
                            //    CacheMemberLoginNameList.Add($"{storeId}|{item.SectionId}|{item.OutSite.LoginName}", item);
                            if (!CacheMemberLoginNameList.ContainsKey($"{item.StoreId}|{item.OutSite.LoginName}"))
                                CacheMemberLoginNameList.Add($"{item.StoreId}|{item.OutSite.LoginName}", item);
                        });
                        break;
                }
            }
        }

        private static void UpdateSectionList(int storeId, List<string> sectionCodes)
        {
            /*
            var sectoinList = db.Set<SectionEntity>().Where(s => s.Status == DataStatus.Normal && s.StoreId == storeId && sectionCodes.Contains(s.SectionCode))
               .Join(db.Set<StoreEntity>().Where(s => s.Status == DataStatus.Normal), sec => sec.StoreId, sto => sto.Id, (sec, sto) => new { sec, sto })
               .Join(db.Set<GroupEntity>().Where(g => g.Status == DataStatus.Normal), s => s.sto.Group_Id, g => g.Id, (s, g) =>
                new SectionTem
                {
                    SectionId = s.sec.Id,
                    SectionCode = s.sec.SectionCode,
                    StoreId = s.sto.Id,
                    BrandCode = s.sec.BrandCode,
                }).ToList();
            */
            /*
            var db = new MagicalHorseContext("MagicalHorseContext");
            var sectionQuery = db.Set<SectionEntity>().Where(s => s.Status == DataStatus.Normal && sectionCodes.Contains(s.SectionCode));
            if (storeId > 0)
                sectionQuery = sectionQuery.Where(s => s.StoreId == storeId);
            var sectoinList = sectionQuery
               .Join(db.Set<StoreEntity>().Where(s => s.Status == DataStatus.Normal), sec => sec.StoreId, sto => sto.Id, (sec, sto) => new { sec, sto })
               .Join(db.Set<GroupEntity>().Where(g => g.Status == DataStatus.Normal), s => s.sto.Group_Id, g => g.Id, (s, g) =>
                new SectionTem
                {
                    SectionId = s.sec.Id,
                    SectionCode = s.sec.SectionCode,
                    StoreId = s.sto.Id,
                    GroupId = s.sto.Group_Id,
                    BrandCode = s.sec.BrandCode,
                }).ToList();*/
            var sectoinList = GetSectionTems(storeId, sectionCodes);
            if (!sectoinList.Any() && sectoinList.Count > 0) return;
            if (CacheSectionList == null) CacheSectionList = new Dictionary<string, SectionTem>();
            if (CacheSectionBrandList == null) CacheSectionBrandList = new Dictionary<string, SectionTem>();
            sectoinList.ForEach(section =>
            {
                if (!CacheSectionList.ContainsKey(section.SectionCode))
                    CacheSectionList.Add(section.SectionCode, section);
                else
                    CacheSectionList[section.SectionCode] = section;

                if (!CacheSectionBrandList.ContainsKey($"{section.SectionCode}|{section.BrandCode}"))
                    CacheSectionBrandList.Add($"{section.SectionCode}|{section.BrandCode}", section);
                else
                    CacheSectionBrandList[$"{section.SectionCode}|{section.BrandCode}"] = section;
            });
        }

        private static void UpdateBuyerList(int storeId, List<string> buyerCodes)
        {
            // var db = new MagicalHorseContext("MagicalHorseContext");
            // //var buyerList = db.Set<IMS_OperatorEntity>().Where(o => buyerCodes.Contains(o.OperatorCode) && o.Status == (int)DataStatus.Normal && o.StoreId == storeId)
            // //    .Select(o => new BuyerTem { BuyerCode = o.OperatorCode, BuyerUserId = o.UserId }).ToList();
            // var query = db.Set<IMS_OperatorEntity>().Where(o => buyerCodes.Contains(o.OperatorCode) && o.Status == (int)DataStatus.Normal);


            var session = DbSessionFactory.GetCurrentDbSession("MagicHorse") as MagicHorseSession;
            var query = session.ImsOperatorRepository.Where(o => buyerCodes.Contains(o.OperatorCode) && o.Status == DataStatus.Normal);

            if (storeId > 0)
                query = query.Where(o => o.StoreId == storeId);
            var buyerList = query.Select(o => new BuyerTem { BuyerCode = o.OperatorCode, BuyerUserId = o.UserId }).ToList();

            if (!buyerList.Any()) return;

            buyerList.ForEach(buyer =>
            {
                if (!CacheBuyerList.ContainsKey(buyer.BuyerCode))
                    CacheBuyerList.Add(buyer.BuyerCode, buyer);
                else
                    CacheBuyerList[buyer.BuyerCode] = buyer;
            });
        }

        public static void UpdateVipLevelList(int storeId)
        {
            var list = pluginDataService.GetStoreVipLevels(storeId);
            if (!list.Any()) return;

            list.ForEach(level =>
            {
                if (!CacheVipLevelList.ContainsKey($"{storeId}|{level.VipLevel}"))
                    CacheVipLevelList.Add($"{storeId}|{level.VipLevel}", level.VipLevelName);
                else
                    CacheVipLevelList[$"{storeId}|{level.VipLevel}"] = level.VipLevelName;
            });
        }




        public static List<SectionTem> GetSectionTems(int storeId, List<string> sectionCodes)
        {
            var session = DbSessionFactory.GetCurrentDbSession("MagicHorse") as MagicHorseSession;
            var sectionQuery = session.SectionRepository.Where(s => s.Status != DataStatus.Deleted && sectionCodes.Contains(s.SectionCode));

            if (storeId > 0)
                sectionQuery = sectionQuery.Where(s => s.StoreId == storeId);
            var sectoinList = sectionQuery
                .Join(session.StoreRepository.Where(s => s.Status == DataStatus.Normal), sec => sec.StoreId, sto => sto.Id, (sec, sto) => new { sec, sto })
                .Join(session.GroupRepository.Where(g => g.Status == DataStatus.Normal), s => s.sto.Group_Id, g => g.Id, (s, g) =>
                    new SectionTem
                    {
                        SectionId = s.sec.Id,
                        SectionCode = s.sec.SectionCode,
                        StoreId = s.sto.Id,
                        GroupId = s.sto.Group_Id,
                        BrandCode = s.sec.BrandCode,
                    }).ToList();

            if (!sectoinList.Any() && sectoinList.Count() > 0) return new List<SectionTem>();
            return sectoinList;
        }


        public static SectionTem CreateSection(int groupId, int storeId, string sectionCode, string sectionName, string brandCode)
        {
            return new SectionTem()
            {
                SectionId = 0,
                SectionCode = sectionCode,
                StoreId = storeId,
                BrandCode = brandCode,
                Msg = $"专柜[{sectionName}]不存在,storeId:{storeId},sectionCode:{sectionCode}"
            };


            var msg = "未知错误";
            var sectionId = 0;
            try
            {
                if (!string.IsNullOrEmpty(sectionCode) && !string.IsNullOrEmpty(sectionName))
                {
                    var session = DbSessionFactory.GetCurrentDbSession("MagicHorse") as MagicHorseSession;
                    var store = session.StoreRepository.Where(s => true).FirstOrDefault(s => s.Status == DataStatus.Normal && s.Id == storeId);

                    var storeOrg = session.OrgInfoRepository.Where(s => s.OrgType == 5 && s.StoreOrSectionID == storeId)
                        .GroupJoin( session.OrgInfoRepository.Where(s => s.OrgType == 10), sto => sto.OrgID, sec => sec.ParentID, (sto, sec) => new { orgId = sto.OrgID, subCount = sec.Count() })
                        .FirstOrDefault();

                    if (store != null && storeOrg != null)
                    {
                        var section = new SectionEntity()
                        {
                            Name = sectionName,
                            Location = "-",
                            ContactPhone = "",
                            ContactPerson = "",
                            SectionCode = sectionCode,
                            StoreId = storeId,
                            BrandCode = "",
                            Status = DataStatus.Normal,
                            CreateUser = 0,
                            CreateDate = DateTime.Now,
                            UpdateUser = 0,
                            UpdateDate = DateTime.Now,
                        };
                        session.SectionRepository.Create(section);
                        //session.SaveChange();

                        var org = new OPC_OrgInfoEntity()
                        {
                            OrgID = $"{storeOrg.orgId}_{(storeOrg.subCount + 1).ToString().PadLeft(3, '0')}",
                            OrgName = section.Name,
                            ParentID = storeOrg.orgId,
                            StoreOrSectionID = section.Id,
                            StoreOrSectionName = "-",
                            OrgType = 10,
                            IsDel = false
                        };
                        session.OrgInfoRepository.Create(org);
                        session.SaveChange();

                        sectionId = section.Id;
                        msg = "添加专柜及组织架构成功";
                    }
                    else if (storeOrg == null)
                        msg = $"专柜创建失败，未找到门店对应的组织";
                    else if (store == null)
                        msg = $"专柜创建失败，未找到门店";
                }
                else if (string.IsNullOrEmpty(sectionCode))
                    msg = "专柜创建失败，专柜编码为空";
                else if (string.IsNullOrEmpty(sectionName))
                    msg = "专柜创建失败，专柜名称为空";

            }
            catch (Exception ex)
            {
                msg = $"Exception ：未知错误 {ex.Message}";
            }

            return new SectionTem()
            {
                SectionId = sectionId,
                SectionCode = sectionCode,
                StoreId = storeId,
                Msg = msg
            };
        }


        public static List<string> GetStroeSectionCodes(int storeId)
        {
            var session = DbSessionFactory.GetCurrentDbSession("MagicHorse") as MagicHorseSession;
            var query = session.SectionRepository.Where(s => s.Status != DataStatus.Deleted && s.StoreId == storeId)
                .Select(s => s.SectionCode);
            if (query.Any())
                return query.ToList();
            return null;
        }

        /// <summary>
        /// GetMemberVipLeavel  from  storeVipLevel
        /// </summary>
        /// <param name="member"></param>
        /// <param name="vipLeavel"></param>
        /// <param name="payTime"></param>
        /// <returns></returns>
        public static string GetMemberVipLeavel(MemberInfo member, string vipLeavel, DateTime payTime)
        {
            if (!CacheVipLevelList.Keys.Select(k => k.StartsWith($"{member.StoreId}|")).Any())
                UpdateVipLevelList(member.StoreId);

            var leavel = vipLeavel;
            var leavelName = "";

            if (string.IsNullOrEmpty(vipLeavel) && member.OutSite != null && member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                var cardLevel = member.OutSite.Detail["CardLevel"];

                var registerTime = DateTime.MinValue;
                if (member.OutSite.Detail.ContainsKey("OriginalRegisterTime"))
                {
                    var str = member.OutSite.Detail["OriginalRegisterTime"];
                    if (DateTime.TryParse(str, out registerTime))
                    {
                        if (registerTime <= payTime)
                        {
                            leavel = cardLevel;
                            //var vipName = CacheVipLevelList.ContainsKey($"{member.StoreId}|{cardLevel}") ? CacheVipLevelList[$"{member.StoreId}|{cardLevel}"] : "";
                            //leavel = !string.IsNullOrWhiteSpace(vipName) ? vipName : cardLevel;
                        }
                        else
                        {
                            leavelName = "非会员";
                        }
                    }
                    else
                    {
                        leavel = cardLevel;
                        //var vipName = CacheVipLevelList.ContainsKey($"{member.StoreId}|{cardLevel}") ? CacheVipLevelList[$"{member.StoreId}|{cardLevel}"] : "";
                        //leavel = !string.IsNullOrWhiteSpace(vipName) ? vipName : cardLevel;
                    }
                }
                else
                {
                    leavelName = "非会员";
                }
            }
            else if (member.OutSite?.Detail == null || !member.OutSite.Detail.ContainsKey("CardLevel"))
            {
                leavelName = "非会员";
            }

            if (string.IsNullOrEmpty(leavelName))
                leavelName = CacheVipLevelList.ContainsKey($"{member.StoreId}|{leavel}") ? CacheVipLevelList[$"{member.StoreId}|{leavel}"] : vipLeavel;
            return leavelName;
        }



        public static void ClearCacheByStoreId(int storeId)
        {
            CacheMemberObjectIdList = CacheMemberObjectIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            CacheMemberIdList = CacheMemberIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            CacheMemberOpenIdList = CacheMemberOpenIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            CacheMemberVipCodeList = CacheMemberVipCodeList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();

            //CacheSectionMemberIdList = CacheSectionMemberIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            //CacheSectionMemberVipCodeList = CacheSectionMemberVipCodeList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            //CacheSectionMemberUserIdList = CacheSectionMemberUserIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();

            CacheBrandMemberIdList = CacheBrandMemberIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            CacheBrandMemberVipCodeList = CacheBrandMemberVipCodeList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();
            //CacheBrandMemberUserIdList = CacheBrandMemberUserIdList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, MemberInfo>();


            CacheVipLevelList = CacheVipLevelList?.Where(dic => !dic.Key.StartsWith($"{storeId}|"))?.ToDictionary(key => key.Key, val => val.Value) ?? new Dictionary<string, string>();


            log.Info($"[{storeId}]DataCache: CacheSectionList={CacheSectionList.Count}, CacheBuyerList={CacheBuyerList.Count}");

            log.Info($"[{storeId}]DataCache: CacheMemberObjectIdList={CacheMemberObjectIdList.Count}, CacheMemberIdList={CacheMemberIdList.Count}, CacheMemberOpenIdList={CacheMemberOpenIdList.Count}" +
                 $"CacheMemberVipCodeList={CacheMemberVipCodeList.Count}, CacheMemberUserIdList={CacheMemberUserIdList.Count}");
        }


    }
}
