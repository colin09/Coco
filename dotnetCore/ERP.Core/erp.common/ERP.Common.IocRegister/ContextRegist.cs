using System;
using Autofac;
using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Log;
using ERP.Domain.Context;
using ERP.Domain.Dapper;

namespace ERP.Common.IocRegister {

    public class ContextRegist : Autofac.Module {

        protected override void Load (ContainerBuilder builder) {

            builder.Register<Logger> ((c) => { return Logger.Current (); }).As<ILogger> ().InstancePerLifetimeScope ();

            builder.RegisterType<ERPContext_Master> ().As<IERPContext> ().InstancePerLifetimeScope ();
            builder.RegisterType<ERPContext_Slave> ().As<IERPSlaveContext> ().InstancePerLifetimeScope ();
            builder.RegisterType<DapperUnitOfWork> ().As<IUnitOfWork> ().InstancePerLifetimeScope ();
        }

    }
}