using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace com.fs.api
{
    public class Startup //: IStartup
    {
        //构造 -> configureService -> configure

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            var log = logger.CreateLogger(nameof(Startup));
            logger.AddProvider();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "my api",
                    Version = "v1",
                    Description = "a simple example for core-api-swagger",
                    TermsOfService = "none",
                    Contact = new Contact { Name = "cc", Email = "xx@mail.com", Url = "https://swagger.io" },
                    License = new License { Name = "under xxx", Url = "https://example.com/license" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "my api v1");
            });
        }
        
    }
}
