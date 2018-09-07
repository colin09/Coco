using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using com.mh.common.extension;
using com.mh.model.enums;
using com.mh.model.mongo.dbMh;
using com.mh.model.mongo.dbSource;
using com.mh.dataengine.common.iservice;
using com.mh.dataengine.common.cache;
using System.Text.RegularExpressions;

namespace com.mh.dataengine.service.transform.mg
{
    class MemberMatchHandlerV1 : OrderMatchBaseHandler
    {


        public static void WriteData(List<string> sectionCodes, DateTime lastTime, Action overAction = null)
        {
            LimitedConcurrencyLevelTaskScheduler scheduler = new LimitedConcurrencyLevelTaskScheduler(3);
            TaskFactory fc = new TaskFactory(scheduler);

            int total = 0, page = 0, pageSize = 5000, pageCount = 0;
            var stores = new List<int>();
            var pageArrar = new List<int>();

            do
            {
                page++;
                var pageIndex = page;

                var list = pluginDataService.ReadMidMemberInfo(lastTime, sectionCodes, pageIndex, pageSize);
                pageCount = list.Count();
                total += pageCount;

                Log.Info($"page : {pageIndex} , count : {pageCount} / {total}");

                var cts = new CancellationTokenSource();
                fc.StartNew(() =>
                {
                    var storeIds = SaveData(list, pageIndex);
                    if (storeIds != null)
                        stores.AddRange(storeIds);

                    pageArrar.Add(pageIndex);
                    Log.Info($"pageArrar:{pageArrar.ToJson()}, pageIndex={pageIndex}");
                    if (pageArrar.Count == page)
                    {
                        Log.Info($"read over, totalCount = {total}, totalpage:{pageIndex}");

                        stores.Distinct().ToList().ForEach(store =>
                        {
                            DataCacheV1.ClearCacheByStoreId(store);
                        });
                        overAction?.Invoke();
                    }
                });
            } while (pageCount > 0);

        }




        static List<int> SaveData(List<dw_memberinfo_source> list, int pageIndex)
        {
            if (list == null || list.Count < 1)
                return null;

            try
            {
                var stores = new List<int>();

                var sectionCodes = list.Select(l => l.store_no).Distinct().ToList();
                var sections = DataCacheV1.GetSectionTems(0, sectionCodes);
                if (sections != null)
                    stores = sections.Select(s => s.StoreId).Distinct().ToList();

                var memberIds = list.Select(m => m.memberid).Distinct().ToList();
                var openIds = list.Select(m => m.member_openid).Distinct().ToList();

                var members = pluginDataService.GetMemberList(0, memberIds: memberIds, openIds: openIds);


                var newMemList = new List<MemberInfo>();
                var modifyMemList = new List<MemberInfo>();
                var changeMemList = new List<MemberInfoVipChange>();

                int newCount = 0, modifyCount = 0;
                foreach (var item in list)
                {
                    //if (item._registTime == null)
                    //{
                    //    Log.Info($"registerTime error: item.ToJson(), {item.memberid}");
                    //    continue;
                    //}
                    if (item.brand_code == "FF" || item.brand_code == "FM")
                        item.brand_code = "15mins";

                    var section = sections.FirstOrDefault(s => s.SectionCode == item.store_no && s.BrandCode == item.brand_code);
                    if (section == null)
                    {
                        //Log.Info($"SectionCode error: item.ToJson() ，{item.store_no} / {item.brand_code}");
                        continue;
                    }
                    MemberInfo mem = null;
                    if (members != null)
                    {
                        mem = members.FirstOrDefault(m => m.MemberId == item.memberid && !string.IsNullOrEmpty(item.memberid) && m.OutSite.BrandCode == item.brand_code);
                        if (mem == null)
                            mem = members.FirstOrDefault(m => m.OutSite.OutSiteUId == item.member_openid && !string.IsNullOrEmpty(item.member_openid) && m.OutSite.BrandCode == item.brand_code);
                    }

                    if (mem != null)
                    {//修改
                        var member = CreateModifyMember(item, mem);
                        modifyMemList.Add(member);
                        modifyCount++;

                        //写 MemberInfoVipChange
                        var oldVipLevel = mem.OutSite?.Detail?.ContainsKey("CardLevel") == true
                            ? mem.OutSite.Detail["CardLevel"]
                            : "";
                        if (item.level_id > 0 && oldVipLevel != item.level_id.ToString())
                        {
                            changeMemList.Add(new MemberInfoVipChange
                            {
                                MemberId = member.MemberId,
                                LastCardLeave = oldVipLevel,
                                CurrentCardLeave = item.level_id.ToString(),
                                ChangeDate = DateTime.Now.AddDays(-1).Date,
                                ChangeFrom = DateTime.Now.AddDays(-1).Date,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                GroupId = member.GroupId,
                                StoreId = member.StoreId,
                                SectionId = section.SectionId
                            });
                        }
                    }
                    else
                    {
                        //新添
                        var member = CreateNewMember(item, section);
                        newMemList.Add(member);
                        newCount++;
                    }
                };
                WriteDb(stores, newMemList, modifyMemList, changeMemList);
                //Log.Info($" newCount : {newMemList.Count}, mCount:{modifyMemList.Count}");
                Log.Info($"page -> {pageIndex} over , newCount : {newCount}, mCount:{modifyCount}");

                return stores;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Log.Info(ex);
            }
            return null;
        }


        static void WriteDb(List<int> stores, List<MemberInfo> newMemList, List<MemberInfo> modifyMemList, List<MemberInfoVipChange> changeList)
        {
            if (newMemList.Any())
                pluginDataService.AddMemberInfos(newMemList);

            if (modifyMemList.Any())
                pluginDataService.ModifyMemberAllInfos(modifyMemList);

            if (changeList.Any())
                pluginDataService.AddMemberVipChangeList(changeList);

            stores.ForEach(storeId =>
            {
                var pressLog = new StoreDataPressLog
                {
                    StoreId = storeId,
                    Time = DateTime.Now.Date,
                    Type = StoreDataPressType.Member,
                    Status = StoreDataPressStatus.UnDeal,
                    CreateDate = DateTime.Now
                };
                storeDataAnchorService.AddStoreDataPressLog(pressLog);
            });



            /**
             * 写 MemberInfoManager
             */
            if (newMemList.Any())
            {
                var managerList = new List<MemberInfoManager>();
                newMemList.ForEach(mem =>
                {
                    managerList.Add(CreateManager(mem));
                });
                pluginDataService.AddMemberManagerList(managerList, storeId: 0);
            }
            if (modifyMemList.Any())
            {
                var ids = modifyMemList.Select(m => m.StringId).ToList();
                var exitIds = pluginDataService.GetMemberManager(ids, storeId: 0);
                var newMag = modifyMemList.Where(m => !exitIds.Contains(m.StringId)).ToList();
                if (newMag != null && newMag.Count > 0)
                {
                    var managerList = new List<MemberInfoManager>();
                    newMag.ForEach(mem =>
                    {
                        managerList.Add(CreateManager(mem));
                    });
                    pluginDataService.AddMemberManagerList(managerList, storeId: 0);
                }
            }
        }



        static MemberInfo CreateNewMember(dw_memberinfo_source item, SectionTem section)
        {
            var newMem = new MemberInfo()
            {
                NickName = item.name,
                Name = item.name,
                GroupId = section.GroupId,
                StoreId = section.StoreId,
                SectionId = section.SectionId,
                BuyerUserId = 0,
                SectionCode = section.SectionCode,
                VipCode = item.level_id > 0 ? item.level_id.ToString() : "",
                CustomerId = 0,
                MemberId = string.IsNullOrEmpty(item.memberid) ? $"" : item.memberid,
                Birthday = item.birthday,
                MobilePhone = item.mobile,
                Gender = item._sex == 0 ? GenderType.Male : item._sex == 1 ? GenderType.Female : GenderType.Default,
                //RegisterTime = item._followTime.HasValue ? (item._registTime < item._followTime.Value ? item._registTime : item._followTime.Value) : item._registTime,
                //RegisterTime = item._registTime ?? item._followTime.Value,

                RegisterTime = item._registTime.HasValue ? item._registTime : new DateTime(2015, 1, 1),
                Email = "",
                Level = 0,
                OutSite = new OutSiteInfo()
                {
                    OutSiteUId = "",
                    OutSiteType = (int)OutsiteType._15Minute,
                    From = "mongoDB",
                    Detail = new Dictionary<string, string>(){
                                { "BrandCode",section.BrandCode },
                                { "StoreNo",section.SectionCode },
                                { "Integral", item.cumulative_amount.ToString() },
                                { "CardLevel", item.level_id.ToString() },
                                { "ValidAmount", item.valid_amount.ToString() },
                            },
                    AviableIntegral = item.valid_amount == null ? 0 : item.valid_amount.Value,
                    TotalIntegral = item.cumulative_amount == null ? 0 : item.cumulative_amount.Value,
                    FreezeIntegral = 0,

                    VipCardLevel = item.level_id.ToString(),
                    VipChangeDate = item._registTime,
                    VipRegisterTime = item._registTime,
                    BrandCode = section.BrandCode,
                    RegisterStoreNo = section.SectionCode

                },
                SubScribe = new SubScribeInfo()
                {
                    IsSubscribe = true,
                    SubscribeTime = item._registTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsScan = false
                },
                SaleData = new SaleDataInfo() { IsBuyer = false },
                Status = (int)DataStatus.Normal,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                CreateUser = 0,
                UpdateUser = 0
            };
            newMem.Id = newMem.CreateObjectId();

            if (item._registTime.HasValue)
            {
                newMem.OutSite.Detail.Add("OriginalRegisterTime", item._registTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //newMem.OutSite.VipRegisterTime = item._registTime;
                //newMem.OutSite.VipChangeDate = item._registTime;
            }

            return newMem;
        }

        static MemberInfo CreateModifyMember(dw_memberinfo_source item, MemberInfo mem)
        {
            //修改vipcode、手机号
            //mem.VipCode = !string.IsNullOrEmpty(item.VipCode) ? item.VipCode : mem.VipCode;
            mem.MobilePhone = !string.IsNullOrEmpty(item.mobile) ? item.mobile : mem.MobilePhone;
            mem.Birthday = item.birthday ?? mem.Birthday;
            mem.Gender = item._sex == 0 ? GenderType.Male : item._sex == 1 ? GenderType.Female : GenderType.Default;
            mem.Name = !string.IsNullOrEmpty(item.name) ? item.name : mem.Name;
            //mem.SectionId = section.sectionId;
            //mem.SectionCode = section.sectionCode;
            mem.RegisterTime = item._registTime ?? new DateTime(2015, 1, 1);
            //mem.RegisterTime = item._registTime ?? item._followTime.Value;

            mem.SubScribe = mem.SubScribe ?? new SubScribeInfo();
            //mem.SubScribe.IsSubscribe = item.UnFollow == "1";
            //mem.SubScribe.SubscribeTime = item._followTime?.ToString("yyyy-MM-dd HH:mm:ss");


            mem.OutSite.VipCardLevel = item.level_id.HasValue ? item.level_id.ToString() : mem.OutSite.VipCardLevel;
            mem.OutSite.AviableIntegral = item.valid_amount.HasValue ? item.valid_amount.Value : mem.OutSite.AviableIntegral;
            mem.OutSite.TotalIntegral = item.cumulative_amount.HasValue ? item.cumulative_amount.Value : mem.OutSite.TotalIntegral;
            mem.OutSite.FreezeIntegral = item.freeze_amount.HasValue ? item.freeze_amount.Value : mem.OutSite.FreezeIntegral;

            if (mem.OutSite.Detail.ContainsKey("Integral"))
            {
                if (item.cumulative_amount.HasValue)
                    mem.OutSite.Detail["Integral"] = item.cumulative_amount.ToString();
            }
            else
                mem.OutSite.Detail.Add("Integral", item.cumulative_amount.ToString());

            if (mem.OutSite.Detail.ContainsKey("CardLevel"))
            {
                if (item.level_id.HasValue)
                    mem.OutSite.Detail["CardLevel"] = item.level_id.ToString();
            }
            else
                mem.OutSite.Detail.Add("CardLevel", item.level_id.ToString());

            if (mem.OutSite.Detail.ContainsKey("ValidAmount"))
            {
                if (item.valid_amount.HasValue)
                    mem.OutSite.Detail["ValidAmount"] = item.valid_amount.ToString();
            }
            else
            {
                mem.OutSite.Detail.Add("ValidAmount", item.valid_amount.ToString());
            }

            if (item._registTime.HasValue)
            {
                if (mem.OutSite.Detail.ContainsKey("OriginalRegisterTime"))
                    mem.OutSite.Detail["OriginalRegisterTime"] = item._registTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    mem.OutSite.Detail.Add("OriginalRegisterTime", item._registTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                mem.OutSite.VipRegisterTime = item._registTime;
            }
            return mem;
        }





        static MemberInfoManager CreateManager(MemberInfo member)
        {
            /*
            var mm = pluginDataService.GetMemberManager(member.StringId, member.SectionId);
            if (mm != null) return null;
            */

            var manager = new MemberInfoManager()
            {
                MemberId = member.StringId,
                BuyerUserId = member.BuyerUserId,
                ManagerLevel = 0,
                Status = MemberManagerState.Normal,
                Logo = "",
                Name = member.Name,
                NickName = member.NickName,
                Description = "",
                LastContactTime = DateTime.MinValue,
                Tags = null,
                Mobile = member.MobilePhone,
                CreateUser = member.CreateUser,
                CreateDate = member.CreateTime,
                UpdateUser = member.UpdateUser,
                UpdateDate = member.UpdateTime,
                StoreId = member.StoreId,
                SectionId = member.SectionId,
                GroupId = member.GroupId,
                VipCode = member.VipCode,
                LastScanTime = getDateTime(member.SubScribe?.LastScanTime ?? ""),
                UpdateLog = new List<MemberManagerLog>()
                {
                    new MemberManagerLog()
                    {
                        MemberId = member.StringId,
                        BuyerUserId = member.BuyerUserId,
                        ManagerLevel = 0,
                        UpdateUser = 0,
                        UpdateDate = DateTime.Now
                    }
                }
            };
            return manager;
        }






        static DateTime getDateTime(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime?.Trim())) return DateTime.MinValue;

            try
            {
                DateTime value;
                Regex rx = new Regex(@"^\d{4}(\-|\/|\.)\d{1,2}(\-|\/|\.)\d{1,2}", RegexOptions.Compiled);
                if (!rx.IsMatch(dateTime.Trim()))
                    return DateTime.MinValue;

                if (DateTime.TryParse(dateTime, out value))
                    return value;
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return DateTime.MinValue;
            }
        }


    }
}