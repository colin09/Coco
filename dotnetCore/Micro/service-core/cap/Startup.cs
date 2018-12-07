using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cap.db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cap {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<HuiContext> ();
            services.AddCap (x => {
                // If you are using EF, you need to add the configuration：
                x.UseEntityFramework<HuiContext> (); //Options, Notice: You don't need to config x.UseSqlServer(""") again! CAP can autodiscovery.

                // If you are using Dapper, you need to add the configuration：
                //x.UseSqlServer ("Your ConnectionStrings");
                //x.UseMySql ("Your ConnectionStrings");
                //x.UsePostgreSql ("Your ConnectionStrings");

                // If you are using MongoDB, you need to add the configuration：
                //x.UseMongoDB ("Your ConnectionStrings"); //MongoDB 4.0+ cluster

                // If you are using RabbitMQ, you need to add the configuration：
                // "amqp://rabbitmq:password@127.0.0.1:5672/;"
                //x.UseRabbitMQ ("127.0.0.1");
                x.UseRabbitMQ (mq=>{
                    mq.HostName="127.0.0.1";
                    mq.UserName ="rabbitmq";
                    mq.Password  ="password";
                    mq.VirtualHost  ="/test";
                    mq.Port = 5672;
                });
                // If you are using Kafka, you need to add the configuration：
                //x.UseKafka ("localhost");

                x.UseDashboard ();
                x.FailedRetryCount = 5;
                x.FailedThresholdCallback = (type, name, content) => {
                    Console.WriteLine ($@"A message of type {type} failed after executing {x.FailedRetryCount} several times, requiring manual troubleshooting. Message name: {name}, message body: {content}");
                };
            });
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}