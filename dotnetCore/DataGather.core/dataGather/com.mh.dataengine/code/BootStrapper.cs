using System;
using Microsoft.Extensions.DependencyInjection;

using com.mh.mongo.iservice;
using com.mh.mongo.service;
using com.mh.common.ioc;
using com.mh.common.Logger;
using com.mh.model.messages.message;
using com.mh.model.messages.datagatherMessage;


using com.mh.rabbit.iservice;
using com.mh.rabbit.message;
using com.mh.rabbit.datagather;

namespace com.mh.dataengine.code
{
    class BootStrapper
    {
        public static void Init()
        {
            RegisterCommonServices();
            RegisterMongoServices();
            RegisterDomainServices();
        }


        private static void RegisterCommonServices()
        {
            IocProvider.RegisterServices(collection =>
            {
                //collection.AddSingleton<ILog, Log4NetLog>();
                collection.AddSingleton<ILog, Logging>();
            });
        }

        private static void RegisterMongoServices()
        {
            IocProvider.RegisterServices(collection =>
            {
                collection.AddSingleton<ICouponService, CouponService>();
                collection.AddSingleton<IPluginDataService, PluginDataService>();
                collection.AddSingleton<IPluginExecuteService, PluginExecuteService>();
                collection.AddSingleton<IPrivilegeService, PrivilegeService>();
                collection.AddSingleton<IStoreDataAnchorService, StoreDataAnchorService>();
                collection.AddSingleton<IStorePreferenceService, StorePreferenceService>();
                collection.AddSingleton<IStrategyActionService, StrategyActionService>();
            });
        }

        private static void RegisterDomainServices()
        {
            IocProvider.RegisterServices(collection =>
            {

                collection.AddSingleton<IMessageProvider<BaseMessage>, MessageProvider>();
                collection.AddSingleton<IMessageProvider<DataGatherBaseMessage>, DataGatherMessageProvider>();


            });
        }


    }
}