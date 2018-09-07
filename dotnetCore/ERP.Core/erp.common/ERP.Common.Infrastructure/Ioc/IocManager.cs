
using System;
using Autofac;
using Autofac.Core;
using System.Reflection;
using System.Collections.Generic;


namespace ERP.Common.Infrastructure.Ioc {
    public class IocManager {
        private IocManager () { }

        private static IocManager _iocManager;


        static IocManager () {
            _iocManager = new IocManager () {
                ContainerBuilder = new ContainerBuilder (),
            };
        }

        public static IocManager Instance => _iocManager;

        public IContainer Container { get; private set; }
        public ContainerBuilder ContainerBuilder { get; private set; }

        
        public IocManager RegisterAutofacModules(params Assembly[] assemblies)
        {
            ContainerBuilder.RegisterAssemblyModules(assemblies);
            return this;
        }






        public IContainer Build()
        {
            if (Container != null) return Container;
            Container = ContainerBuilder.Build();
            return Container;
        }



        public static TService Resolve<TService>()
        {
            var scope = IocScope.CurrentLifetimeScope;
            if (scope != null)
              return  scope.Resolve<TService>();
            return IocManager.Instance.Container.Resolve<TService>();
        }

        public static TService Resolve<TService>(string serviceName)
        {
            var scope = IocScope.CurrentLifetimeScope;
            if (scope != null)
                return scope.Resolve<TService>(serviceName);
            return IocManager.Instance.Container.ResolveNamed<TService>(serviceName);
        }

        public static TService Resolve<TService>(IEnumerable<Parameter> parameters)
        {
            var scope = IocScope.CurrentLifetimeScope;
            if (scope != null)
                return scope.Resolve<TService>(parameters);
            return IocManager.Instance.Container.Resolve<TService>(parameters);
        }

        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            var scope = IocScope.CurrentLifetimeScope;
            if (scope != null)
                return scope.Resolve<TService>(parameters);
            return IocManager.Instance.Container.Resolve<TService>(parameters);
        }





        public static IocScope BeginLifetimeScope()
        {
            var scope = IocManager.Instance.Container.BeginLifetimeScope();
            return IocScope.CreateLifetimeScope(scope);
        }
    }
}