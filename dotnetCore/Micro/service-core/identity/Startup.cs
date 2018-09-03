using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundatio.Caching;
using identity.Model;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Steeltoe.Discovery.Client;

namespace identity {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDiscoveryClient (Configuration);
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            var config = new Config (Configuration);
            //var redisconnectionString = Configuration.GetConnectionString ("RedisConnectionString");
            services.AddIdentityServer (x => {
                    x.IssuerUri = "http://identity";
                    x.PublicOrigin = "http://identity";
                })
                .AddDeveloperSigningCredential ()
                .AddInMemoryPersistedGrants ()
                .AddInMemoryApiResources (config.GetApiResources ())
                .AddInMemoryClients (config.GetClients ())
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            /* 
            services.AddSingleton (ConnectionMultiplexer.Connect (redisconnectionString));
            services.AddTransient<ICacheClient, RedisCacheClient> (); //注入redis
            services.AddSingleton<IPersistedGrantStore, RedisPersistedGrantStore> ();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator> ();
            */
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
            app.UseIdentityServer ();
        }
    }
}