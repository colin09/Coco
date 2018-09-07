using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using com.mh.common.email;
using com.mh.common.extension;
using com.mh.model.mongo.dbMh;
using com.mh.model.enums;
using com.mh.mysql.factory;
using com.mh.mysql.repository.dbSession;
using com.mh.dataengine.common.iservice;
using com.mh.dataengine.common.cache;


namespace com.mh.dataengine.service.transform.mg
{

    public class MgDataTransformV1 : BaseDataEngine, IDataEngine
    {
        public override int RuleId
        {
            get { return 210; }
        }


        public override string Operate
        {
            get { return "mapping"; }
        }

        public string BrandKey { set; get; }




        public string Execute(string dataSource)
        {
            var result = "";

            log.Info($"LastId:{LastId} , stores : {Config["PathKey"]},  passBrandCode: {Config["BucketName"]}");

            //var sectionCodes = com.magicalhorse.fashion.common.ConfigManager.GXGSectionCode;
            //if (!string.IsNullOrEmpty(sectionCodes))
            //{
            //    var codes = sectionCodes.Split(',');
            //    codes.ToList().ForEach(code => { WriteDB(code, 0); });
            //}

            if (LastId >= 0)
            {
                var lastTime = DateTime.FromOADate(LastId);
                if (lastTime < new DateTime(1753, 1, 1))
                    lastTime = new DateTime(2000, 1, 1);

                var sectionCodes = GetReadSectionCodes();

                //会员
                pluginDataService.ModifyStoreDataRuleLastId(StoreId, lastTime.ToOADate() * -1);
                MemberMatchHandlerV1.WriteData(sectionCodes, lastTime, () =>
                {
                    //订单
                    WriteDB(sectionCodes, lastTime);
                });

                //WriteDB(sectionCodes, lastTime);
            }
            return result;
        }





        private void WriteDB(List<string> sectionCodes, DateTime startTime)
        {
            var lastTime = startTime;
            var passBrandCode = string.IsNullOrEmpty(Config["BucketName"]) ? new List<string>() : Config["BucketName"].Split(',').ToList();
            log.Info($"start read order from mg ==> section-code.count: [{sectionCodes.Count()}], list-time: [{lastTime}], pass-brand-code: {passBrandCode.ToJson()}");

            #region do
            var totalPage = 1;
            var pageSize = 5000;
            var pageM = 1; var pageSizeM = 5000 * 100;
            var dic = new Dictionary<int, string>();
            var newSectionCodes = new List<string>();

            var pageInit = 1;
            Int32.TryParse(Config["EndPoint"], out pageInit);

            pageM = pageInit;
            totalPage = (pageInit - 1) * 100;

            //var convert = new ModelConvert<PluginOrderTxt>(Mapping);
            var totalCount = 0; var errorCount = 0; var readCount = 0; var lastId = 0;
            var pageArrar = new List<int>();
            var maxId = "";
            var allId = new List<string>();

            List<CancellationTokenSource> ctsList = new List<CancellationTokenSource>();

            LimitedConcurrencyLevelTaskScheduler scheduler = new LimitedConcurrencyLevelTaskScheduler(5);
            TaskFactory fc = new TaskFactory(scheduler);
            do
            {
                var noSectionCodes = new List<string>();
                var list = new List<PluginOrderTxt>();
                var pIndex = totalPage + 1;
                /*
                try
                { 
                    //list = pluginDataService.ReadMidOrderList(lastTime, totalCount, sectionCodes, maxId, page, pageSize);
                    //maxId = list.Max(l => l.billingcode);
                }
                catch (Exception ex)
                {
                    Log.Info(ex);
                    Log.Info($"page : {pIndex} , get mg error.");  //return;
                }*/

                if (allId == null || allId.Count < 1)
                {
                    allId = pluginDataService.ReadMidOrderIds(startTime, sectionCodes, pageM, pageSizeM);
                    log.Info($"pageM -> {pageM} Id count : {allId.Count}");
                    pageM++;
                }

                var pageIds = allId.Take(pageSize).ToList();
                allId.RemoveRange(0, pageIds.Count);

                if (pageIds.Any())
                {
                    list = pluginDataService.ReadMidOrderList(totalCount, pageIds);
                }

                totalPage++;
                readCount = list.Count();
                log.Info($"page : {pIndex} , count : {readCount}, maxId : {maxId}");
                totalCount += readCount;

                //list = list.OrderByDescending(l => l.couponcode).ToList();

                var cts = new CancellationTokenSource();
                fc.StartNew(() =>
                {
                    var makeError = OrderMatchHandlerV1.WriteData(list, $"mongoDB-{totalPage}", passBrandCode, out noSectionCodes);
                    if (makeError.Keys.Count > 0)
                    {
                        dic =dic.Merge(makeError);
                        errorCount += makeError.Count();
                    }
                    if (noSectionCodes.Count > 0)
                        newSectionCodes.AddRange(noSectionCodes);
                    if (list.Any())
                    {
                        var maxTime = list.Max(m => m.ctime);
                        if (maxTime > lastTime) lastTime = maxTime;
                    }
                    pluginDataService.ModifyStoreDataRuleLastId(StoreId, lastTime.ToOADate() * -1);
                    log.Info($"page {pIndex} over,  errorCount = {makeError.Keys.Count}, lastId: {lastId}");

                    if (dic.Count > 5000)
                    {
                        SendResultEMail(totalCount, 0, dic, true);
                        //释放内存
                        dic = new Dictionary<int, string>();
                    }

                    pageArrar.Add(pIndex);
                    log.Info($"pageInit={pageInit}, pageIndex={pIndex}, totalPage={totalPage}, pageArrar:{pageArrar.ToJson()}");
                    if (pageArrar.Count == totalPage - (pageInit - 1) * 100)
                    {
                        log.Info($"all read over, totalCount = {totalCount}, errorCount = {errorCount}, storeId:{StoreId}, lastId: {lastId}, page:{totalPage}");

                        //记录lastId
                        pluginDataService.ModifyStoreDataRuleLastId(StoreId, lastTime.ToOADate());
                        //发送邮件
                        if (totalCount > 0)
                        {
                            SendResultEMail(totalCount, errorCount, dic, false);
                            pluginDataService.ModifyStoreLastSyncTime(StoreId);
                        }

                        //清除缓存
                        DataCacheV1.ClearCacheByStoreId(StoreId);
                    }
                }, cts.Token);
                ctsList.Add(cts);
            } while (readCount > 0);
            #endregion
            /*
            Log.Info($"all read over, totalCount = {totalCount}, errorCount = {errorCount}, storeId:{StoreId}, lastId: {lastId}, page:{page}");

            //记录lastId
            pluginDataService.ModifyStoreDataRuleLastId(StoreId, lastTime.ToOADate());
            //发送邮件
            if (totalCount > 0)
            {
                SendResultEMail(totalCount, errorCount, dic, false);
                pluginDataService.ModifyStoreLastSyncTime(StoreId);
            }

            //清除缓存
            DataCacheV1.ClearCacheByStoreId(StoreId);
            */
        }



        private void SendResultEMail(int totalCount, int errorCount, Dictionary<int, string> error, bool isPart)
        {
            if (string.IsNullOrEmpty(MailAddress))
                return;

            var title = $"百丽 订单导入情况-{DateTime.Now.ToString("yyyy-MM-dd")}";
            title += isPart ? "(未完)" : "(完成)";

            var errorCnt = error?.Keys.Count;
            var content = $"今日截止{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}从 中间库共计读取{totalCount}行数据，本次成功导入{totalCount - errorCnt}行数据；";
            /*  不在记录专柜不存在错误
           if (newSectionCode.Count > 0)
           {
               content += $"<br /><br /> 检测到{newSectionCode.Distinct().ToList().Count}个不存在的专柜编码，共计{newSectionCode.Count}行数据；";
               content += $"<br /><br /> 不存在的专柜编码：";
               newSectionCode.Distinct().ToList().ForEach(code =>
               {
                   content += $"<br /><br />   {code}";
               });
           }*/
            if (!isPart)
                content += $"<br /><br /> 今日数据已全部读取完成，共计导入{totalCount - errorCount}行订单数据。";

            if (errorCount > 0)
            {
                content += "<br /><br />  ";
                var array = error.Select(e => $"{e.Key} ---> {e.Value}").ToList();
                content += string.Join("<br /><br /> ", array);
            }
            log.Info($"send email to {MailAddress}, copy to {MailCopyAddress}");
            EMailClient.SendByReport(MailAddress, title, content, MailCopyAddress, new List<string>());
        }


        private List<string> GetReadSectionCodes()
        {
            var storeStr = Config["PathKey"];
            if (string.IsNullOrEmpty(storeStr))
                return null;

            var stores = new List<int>();
            var array = storeStr.Split(',');
            array.ToList().ForEach(s =>
            {
                stores.Add(Convert.ToInt32(s));
            });

            var startDate = new DateTime(2016, 1, 1);
            var session = DbSessionFactory.GetCurrentDbSession("MagicHorse") as MagicHorseSession;
            var sections = session.SectionRepository.Where(s => s.Status != DataStatus.Deleted && s.CreateDate >= startDate && stores.Contains(s.StoreId.Value));
            var sectionCodes = sections.Select(s => s.SectionCode).Distinct().ToList();

            System.Console.WriteLine($"stores : {stores.ToJson()}, section.Codes count : {sectionCodes.Count()}");
            return sectionCodes;
        }

    }

}