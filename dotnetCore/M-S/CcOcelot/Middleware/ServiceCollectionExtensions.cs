using System;
using CcOcelot.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;

namespace CcOcelot.Middleware {
    public static class ServiceCollectionExtensions {
        public static IOcelotBuilder AddCcOcelot (this IOcelotBuilder builder, Action<CcOcelotSqlConfig> option) {

            builder.Services.Configure (option);

            builder.Services.AddSingleton (resolver => resolver.GetService<IOptions<CcOcelotSqlConfig>> ().Value);
            //配置文件仓储注入
            builder.Services.AddSingleton<IFileConfigurationRepository, ServerFileConfigurationRepository> ();
            return builder;
        }
    }
}