using System;
using Autofac;
using ERP.Domain.Context;

using Dappers = ERP.Domain.Dapper;
using Repository = ERP.Domain.Repository;

namespace ERP.Common.IocRegister {

    public class RepositoryRegist : Autofac.Module {

        protected override void Load (Autofac.ContainerBuilder builder) {

            builder.RegisterType<Dappers.AmoebaModule.AmoebaReportModule.AmoebaReportRepository> ().As<Repository.AmoebaModule.AmoebaReportModule.IAmoebaReportRepository> ();
            builder.Register<Dappers.AmoebaModule.AmoebaReportModule.AmoebaReportRepository> ((c) => { return new Dappers.AmoebaModule.AmoebaReportModule.AmoebaReportRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.AmoebaModule.AmoebaReportModule.IAmoebaReportQueryRepository> ();

            builder.RegisterType<Dappers.AmoebaModule.AmoebaConfigModule.AmoebaConfigRepository> ().As<Repository.AmoebaModule.AmoebaConfigModule.IAmoebaConfigRepository> ();
            builder.Register<Dappers.AmoebaModule.AmoebaConfigModule.AmoebaConfigRepository> ((c) => { return new Dappers.AmoebaModule.AmoebaConfigModule.AmoebaConfigRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.AmoebaModule.AmoebaConfigModule.IAmoebaConfigQueryRepository> ();

            builder.RegisterType<Dappers.AmoebaModule.AmoebaTargetModule.AmoebaTargetRepository> ().As<Repository.AmoebaModule.AmoebaTargetModule.IAmoebaTargetRepository> ();
            builder.Register<Dappers.AmoebaModule.AmoebaTargetModule.AmoebaTargetRepository> ((c) => { return new Dappers.AmoebaModule.AmoebaTargetModule.AmoebaTargetRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.AmoebaModule.AmoebaTargetModule.IAmoebaTargetQueryRepository> ();

            builder.RegisterType<Dappers.AmoebaModule.AmoebaImprovementModule.AmoebaImprovementRepository> ().As<Repository.AmoebaModule.AmoebaImprovementModule.IAmoebaImprovementRepository> ();
            builder.Register<Dappers.AmoebaModule.AmoebaImprovementModule.AmoebaImprovementRepository> ((c) => { return new Dappers.AmoebaModule.AmoebaImprovementModule.AmoebaImprovementRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.AmoebaModule.AmoebaImprovementModule.IAmoebaImprovementQueryRepository> ();

            builder.RegisterType<Dappers.OrgModule.CityModule.CityRepository> ().As<Repository.OrgModule.CityModule.ICityRepository> ();
            builder.Register<Dappers.OrgModule.CityModule.CityRepository> ((c) => { return new Dappers.OrgModule.CityModule.CityRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.OrgModule.CityModule.ICityQueryRepository> ();

            builder.RegisterType<Dappers.OrgModule.OrgRepository> ().As<Repository.OrgModule.IOrgRepository> ();
            builder.Register<Dappers.OrgModule.OrgRepository> ((c) => { return new Dappers.OrgModule.OrgRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.OrgModule.IOrgQueryRepository> ();

            builder.RegisterType<Dappers.OrgModule.ProviderModule.ProviderRepository> ().As<Repository.OrgModule.ProviderModule.IProviderRepository> ();
            builder.Register<Dappers.OrgModule.ProviderModule.ProviderRepository> ((c) => { return new Dappers.OrgModule.ProviderModule.ProviderRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.OrgModule.ProviderModule.IProviderQueryRepository> ();

            builder.RegisterType<Dappers.OrgModule.ShopModule.ShopRepository> ().As<Repository.OrgModule.ShopModule.IShopRepository> ();
            builder.Register<Dappers.OrgModule.ShopModule.ShopRepository> ((c) => { return new Dappers.OrgModule.ShopModule.ShopRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.OrgModule.ShopModule.IShopQueryRepository> ();

            builder.RegisterType<Dappers.OrgModule.StoreHouseModule.StoreHouseRepository> ().As<Repository.OrgModule.StoreHouseModule.IStoreHouseRepository> ();
            builder.Register<Dappers.OrgModule.StoreHouseModule.StoreHouseRepository> ((c) => { return new Dappers.OrgModule.StoreHouseModule.StoreHouseRepository (c.Resolve<IERPSlaveContext> ()); }).As<Repository.OrgModule.StoreHouseModule.IStoreHouseQueryRepository> ();

        }

    }

}