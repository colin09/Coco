using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Client;

namespace order {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDiscoveryClient (Configuration);
            var discoveryClient = services.BuildServiceProvider ().GetService<IDiscoveryClient> ();
            var handler = new DiscoveryHttpClientHandler (discoveryClient);
            services.AddAuthorization ();

            services.AddAuthentication (x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication (x => {
                    x.ApiName = "api1";
                    x.ApiSecret = "secret";
                    x.Authority = "http://identity";
                    x.RequireHttpsMetadata = false;
                    x.JwtBackChannelHandler = handler;
                    x.IntrospectionDiscoveryHandler = handler;
                    x.IntrospectionBackChannelHandler = handler;
                });
            /*
            services.AddAuthentication ("Bearer") //添加授权模式
                .AddIdentityServerAuthentication (Options => {
                    Options.Authority = "http://localhost:5006"; //授权服务器地址
                    Options.RequireHttpsMetadata = false; //是否是https
                    Options.ApiName = "api";
                });*/
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }
            app.UseAuthentication ();
            app.UseHttpsRedirection ();
            app.UseMvc ();
            app.UseDiscoveryClient ();
        }
    }
}