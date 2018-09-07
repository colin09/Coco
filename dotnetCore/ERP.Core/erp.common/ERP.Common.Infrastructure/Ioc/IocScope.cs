using System;
using Autofac;
using System.Collections.Generic;
using ERP.Common.Infrastructure.Utility;

namespace ERP.Common.Infrastructure.Ioc {

    public class IocScope : ILifetimeScope {

        private Autofac.ILifetimeScope scope;

        private IocScope (Autofac.ILifetimeScope scope) {
            this.scope = scope;
        }

        public static IocScope CreateLifetimeScope (Autofac.ILifetimeScope scope) {
            var iocScope = new IocScope (scope);
            CallContextUtility.SetData<IocScope> (iocScope);
            return iocScope;
        }

        public static IocScope CurrentLifetimeScope {
            get { return CallContextUtility.GetData<IocScope> (); }
        }

        public TService Resolve<TService> () {
            return this.scope.Resolve<TService> ();
        }

        public TService Resolve<TService> (string serviceName) {
            return this.scope.ResolveNamed<TService> (serviceName);
        }

        public TService Resolve<TService> (IEnumerable<Autofac.Core.Parameter> parameters) {
            return this.scope.Resolve<TService> (parameters);
        }

        public TService Resolve<TService> (params Autofac.Core.Parameter[] parameters) {
            return this.scope.Resolve<TService> (parameters);
        }

        private bool disposed = false;
        public void Dispose () {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        protected virtual void Dispose (bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    CallContextUtility.ClearData<IocScope> ();
                    scope.Dispose ();
                }
            }
            this.disposed = true;
        }

    }
}