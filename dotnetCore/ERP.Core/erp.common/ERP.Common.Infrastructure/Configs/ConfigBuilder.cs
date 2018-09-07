using System;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace ERP.Common.Infrastructure.Configs {

    public class ConfigBuilder {

        public IConfigurationRoot Configuration { get; set; }

        public ConfigBuilder () {
            var builder = new ConfigurationBuilder ()
                .SetBasePath ($"{Directory.GetCurrentDirectory()}/configurations/")
                //.SetBasePath (Directory.GetCurrentDirectory())
                .AddJsonFile ("appsettings.json");

            Configuration = builder.Build ();
        }
    }
}