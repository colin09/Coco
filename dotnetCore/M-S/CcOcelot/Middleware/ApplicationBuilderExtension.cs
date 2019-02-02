using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Configuration.Setter;
using Ocelot.DependencyInjection;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Middleware.Pipeline;
using Ocelot.Responses;

namespace CcOcelot.Middleware {
    public static class ApplicationBuilderExtension {
        public static async Task<IApplicationBuilder> UseCcOcelot (this IApplicationBuilder builder) {
            await builder.UseCcOcelot (new OcelotPipelineConfiguration ());
            return builder;
        }

        public static async Task<IApplicationBuilder> UseCcOcelot (this IApplicationBuilder builder, Action<OcelotPipelineConfiguration> pipelineConfiguration) {
            var config = new OcelotPipelineConfiguration ();
            pipelineConfiguration?.Invoke (config);
            return await builder.UseCcOcelot (config);
        }

        public static async Task<IApplicationBuilder> UseCcOcelot (this IApplicationBuilder builder, OcelotPipelineConfiguration pipelineConfiguration) {
            //重写创建配置方法
            var configuration = await CreateConfiguration (builder);

            ConfigureDiagnosticListener (builder);

            return CreateOcelotPipeline (builder, pipelineConfiguration);
        }

        private static IApplicationBuilder CreateOcelotPipeline (IApplicationBuilder builder, OcelotPipelineConfiguration pipelineConfiguration) {
            var pipelineBuilder = new OcelotPipelineBuilder (builder.ApplicationServices);

            pipelineBuilder.BuildOcelotPipeline (pipelineConfiguration);

            var firstDelegate = pipelineBuilder.Build ();

            /*
            inject first delegate into first piece of asp.net middleware..maybe not like this
            then because we are updating the http context in ocelot it comes out correct for
            rest of asp.net..
            */

            builder.Properties["analysis.NextMiddlewareName"] = "TransitionToOcelotMiddleware";

            builder.Use (async (context, task) => {
                var downstreamContext = new DownstreamContext (context);
                await firstDelegate.Invoke (downstreamContext);
            });

            return builder;
        }

        private static async Task<IInternalConfiguration> CreateConfiguration (IApplicationBuilder builder) {
            //提取文件配置信息
            var fileConfig = await builder.ApplicationServices.GetService<IFileConfigurationRepository> ().Get ();
            var internalConfigCreator = builder.ApplicationServices.GetService<IInternalConfigurationCreator> ();
            var internalConfig = await internalConfigCreator.Create (fileConfig.Data);
            //如果配置文件错误直接抛出异常
            if (internalConfig.IsError) {
                ThrowToStopOcelotStarting (internalConfig);
            }

            //配置信息缓存，这块需要注意实现方式，因为后期我们需要改造下满足分布式架构,这篇不做讲解
            var internalConfigRepo = builder.ApplicationServices.GetService<IInternalConfigurationRepository> ();
            internalConfigRepo.AddOrReplace (internalConfig.Data);
            return GetOcelotConfigAndReturn (internalConfigRepo);
        }

        private static bool IsError (Response response) {
            return response == null || response.IsError;
        }

        private static IInternalConfiguration GetOcelotConfigAndReturn (IInternalConfigurationRepository provider) {
            var ocelotConfiguration = provider.Get ();

            if (ocelotConfiguration?.Data == null || ocelotConfiguration.IsError) {
                ThrowToStopOcelotStarting (ocelotConfiguration);
            }

            return ocelotConfiguration.Data;
        }

        private static void ThrowToStopOcelotStarting (Response config) {
            throw new Exception ($"Unable to start Ocelot, errors are: {string.Join(",", config.Errors.Select(x => x.ToString()))}");
        }

        private static void ConfigureDiagnosticListener (IApplicationBuilder builder) {
            var env = builder.ApplicationServices.GetService<IHostingEnvironment> ();
            var listener = builder.ApplicationServices.GetService<OcelotDiagnosticListener> ();
            var diagnosticListener = builder.ApplicationServices.GetService<DiagnosticListener> ();
            diagnosticListener.SubscribeWithAdapter (listener);
        }
    }
}