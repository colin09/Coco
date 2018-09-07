using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace com.mh.common.configuration
{

    class ConfigBuilder
    {

        public IConfigurationRoot Configuration { get; set; }

        public ConfigBuilder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/configurations/")
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    }


}