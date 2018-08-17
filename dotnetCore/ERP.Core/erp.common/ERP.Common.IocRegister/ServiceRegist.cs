using System;
using Autofac;

using IService = ERP.Domain.IService;
using Services = ERP.Domain.Services;

namespace ERP.Common.IocRegister {
    public class ServiceRegist : Autofac.Module {

        protected override void Load (ContainerBuilder builder) {

            builder.RegisterType<Services.AmoebaModule.AmoebaReportModule.AmoebaReportService> ().As<IService.AmoebaModule.AmoebaReportModule.IAmoebaReportService> ().InstancePerLifetimeScope ();
            builder.RegisterType<Services.AmoebaModule.AmoebaReportModule.AmoebaReportQueryService> ().As<IService.AmoebaModule.AmoebaReportModule.IAmoebaReportQueryService> ().InstancePerLifetimeScope ();

        }

    }
}