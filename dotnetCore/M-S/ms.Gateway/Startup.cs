﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CcOcelot.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace ms.Gateway {
    public class Startup {

        public IConfiguration Configuration { get; }
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            services.AddOcelot ().AddCcOcelot (option => {
                option.DbConnectionStrings = "Server=127.0.0.1;Database=Ocelot;User ID=root;Password=root-1234;";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseCcOcelot().Wait();

            app.Run (async (context) => {
                await context.Response.WriteAsync ("Hello Start!");
            });
        }
    }
}