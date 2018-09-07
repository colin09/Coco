using System;
using Quartz;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using com.mh.common.ioc;
using com.mh.common.Logger;
using com.mh.mongo.iservice;
using com.mh.model.mongo.dbMh;
using com.mh.dataengine.common.iservice;

namespace com.mh.dataengine.job
{

    class DataGatherJob : IJob
    {

        private static ILog log = IocProvider.GetService<ILog>();
        private static IPluginDataService DataService => IocProvider.GetService<IPluginDataService>();

        public async Task Execute(IJobExecutionContext context)
        {
            log.Info("########## DataGatherJob execute ########################################################");

            var serviceList = LoadAllAssembly();

            var ruleList = DataService.GetStoreDataRules(); //"datagather"
            ruleList.ForEach(item =>
                {
                    log.Info($"====>> storeId={item.StoreId},groupId={item.GroupId},rule.count={item.RunStrategy.Count}");
                    Action(item, serviceList);
                });
        }


        public void Action(StoreDataGather storeRule, List<ReflectionM> serviceList)
        {
            if (storeRule == null || storeRule.RunStrategy == null)
                log.Info($"数据获取执行策略为空。");
            storeRule.RunStrategy.ForEach(strategy =>
            {
                if (strategy.SubModule == null || strategy.SubModule.Count < 1)
                    log.Info($"执行插件未定义。");

                var readResult = "";
                strategy.SubModule.OrderBy(r => r.ModuleId).ToList().ForEach(item =>
                {
                    var tService = serviceList.FirstOrDefault(s => s.RuleId == item.ModuleId);
                    if (tService != null)
                    {
                        var service = (ReflectionM)tService.Clone();
                        var server = service.Service;
                        //var instance = service.Instance;
                        var instance = Activator.CreateInstance(server);
                        var method = service.Method;

                        var id = server.GetProperty("GroupId");
                        id.SetValue(instance, storeRule.GroupId);

                        var type = server.GetProperty("StoreId");
                        type.SetValue(instance, storeRule.StoreId);

                        var config = server.GetProperty("Config");
                        config.SetValue(instance, strategy.Config);

                        var mapping = server.GetProperty("Mapping");
                        mapping.SetValue(instance, strategy.Mapping);

                        var mails = server.GetProperty("EMails");
                        mails.SetValue(instance, storeRule.EMails);

                        var lastId = server.GetProperty("LastId");
                        lastId.SetValue(instance, storeRule.LastId);

                        try
                        {
                            object result = "";
                            switch (service.Operate)
                            {
                                case "read":
                                    readResult = method.Invoke(instance, new object[] { "" }).ToString();
                                    result = readResult;
                                    break;

                                case "mapping":

                                    var brand = server.GetProperty("BrandKey");
                                    brand.SetValue(instance, storeRule.RuleKey);

                                    result = method.Invoke(instance, new object[] { readResult });
                                    break;

                                case "write":
                                    result = method.Invoke(instance, new object[] { readResult });
                                    break;
                            }

                        }
                        catch (Exception ex)
                        {
                            log.Info($"method.Invoke exception: [{service.Operate}] : {ex}");
                        }
                        //_log.Info($"{server.Name} service result:{result}");
                    }else{
                        System.Console.WriteLine($"Module { item.ModuleId} not found.");
                    }
                });

            });
        }







        public List<ReflectionM> LoadAllAssembly()
        {
            List<ReflectionM> serviceList = new List<ReflectionM>();

            //_log.Info($"Environment.CurrentDirectory = {Environment.CurrentDirectory}");
            //_log.Info($"System.IO.Path.GetFullPath()= {System.IO.Path.GetFullPath(".\\")}");
            //_log.Info($"AppDomain.CurrentDomain.BaseDirectory = {AppDomain.CurrentDomain.BaseDirectory}");

            var executePath = AppDomain.CurrentDomain.BaseDirectory;

            List<string> filelist = new List<string>(System.IO.Directory.GetFiles(executePath, "*.dll"));
            filelist = filelist.Where(f => f.ToLower().EndsWith(".dll") && f.ToLower().Contains("com.mh.dataengine.service")).ToList();

            foreach (var item in filelist)
            {
                var file = Path.GetFileName(item).ToLower();
                log.Info($"file-name = {file}");
                try
                {
                    // if (file.EndsWith(".dll") && file.StartsWith("com.mh.dataengine.service"))
                    // {}
                    var services = Assembly.LoadFrom(executePath + file).GetTypes()
                        .Where(type => !String.IsNullOrEmpty(type.Namespace))
                        .Where(type => type.BaseType != null && type.BaseType == typeof(BaseDataEngine));

                    foreach (var svc in services)
                    {
                        object obj = Activator.CreateInstance(svc);
                        var method = svc.GetMethod("Execute");
                        serviceList.Add(new ReflectionM
                        {
                            RuleId = Convert.ToInt32(svc.GetProperty("RuleId").GetValue(obj)),
                            Operate = svc.GetProperty("Operate").GetValue(obj).ToString(),
                            Service = svc,
                            Instance = obj,
                            Method = method
                        });
                        log.Info($"   ==>>  service name : {svc.Name}");
                    }
                }
                catch (Exception ex)
                {
                    log.Info(" " + ex.Message);
                }
            }
            return serviceList;
        }

    }



    public class ReflectionM : ICloneable
    {
        public object Instance { set; get; }
        public Type Service { set; get; }
        public MethodInfo Method { set; get; }


        public int RuleId { set; get; }
        public string Operate { set; get; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
