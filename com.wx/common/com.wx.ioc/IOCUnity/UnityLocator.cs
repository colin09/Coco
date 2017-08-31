using com.wx.ioc.IOCAdapter;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace com.wx.ioc.IOCUnity
{
    public class UnityLocator : BaseLocator
    {
        private readonly IUnityContainer _container;
        public UnityLocator()
            : this(new UnityContainer()) //--> this 指向当前类，所以此处即为调用当前类的有一个参数的构造方法
        {
        }
        public UnityLocator(IUnityContainer container)
        {
            _container = container;

            if (ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) != null)
            {
                try
                {
                    var configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
                    configuration.Configure(container, "defaultContainer");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public override string Name
        {
            get { return "Unity"; }
        }

        /// <summary>
        /// 通过<paramref name="key"/>作为键值向容器中注册 单例服务类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TType"></typeparam>
        /// <param name="key"></param>
        protected override void DoRegisterSingleton<TService, TType>(string key)
        {
            _container.RegisterType<TService, TType>(key, new ContainerControlledLifetimeManager());
        }

        protected override void DoRegister<TService, TType>(string key)
        {
            _container.RegisterType<TService, TType>(key);
        }

        protected override TService DoResolve<TService>(string key)
        {
            try
            {
                return _container.Resolve<TService>(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override object DoResolve(Type type)
        {
            return _container.Resolve(type);
        }

        protected override IEnumerable<object> DoResolveAll(Type type)
        {
            return _container.ResolveAll(type);
        }
    }
}
