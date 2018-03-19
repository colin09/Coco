using System;
using Microsoft.Extensions.DependencyInjection;

using com.mh.mongo.iservice;
using com.mh.mongo.service;

namespace com.mh.test.ioc
{
    class DIBootStrapper
    {
        private static IServiceProvider _container;



        static DIBootStrapper()
        {
            RegisterServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IPluginDataService, PluginDataService>();
            //services.AddTransient<HelloController>();
            _container = services.BuildServiceProvider();

            System.Console.WriteLine("RegisterServices");
        }

        public static IServiceProvider Container
        {
            get { return _container; }
        }

        public static void Init()
        {
            RegisterServices();
        }

    }
}