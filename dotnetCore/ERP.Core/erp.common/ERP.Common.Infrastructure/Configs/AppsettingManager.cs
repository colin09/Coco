using System;
using System.IO;
// Requires NuGet package
// Microsoft.Extensions.Configuration.Json
using Microsoft.Extensions.Configuration;

namespace ERP.Common.Infrastructure.Configs {

    public class AppsettingManager {

        //private static IConfiguration _configuration { get; set; }
        private static IConfigurationRoot Configuration = new ConfigBuilder ().Configuration;

        #region - test -
        public static string TestJson {
            get {
                var jsonS = "";
                jsonS += $"option1 : {Configuration["option1"]} \n";
                jsonS += $"option2 : {Configuration["option2"]} \n";

                jsonS += $"subsection.suboption1 : {Configuration["subsection:suboption1"]} \n";

                jsonS += $"wizards[1].Name : {Configuration["wizards:0:Name"]} \n";
                jsonS += $"wizards[1].Age : {Configuration["wizards:0:Age"]} \n";

                jsonS += $"wizards[2].Name : {Configuration["wizards:1:Name"]} \n";
                jsonS += $"wizards[2].Age : {Configuration["wizards:1:Age"]} \n";

                return jsonS;
            }
        }

        /*
        "wizards": [
            {
                "Name": "Gandalf",
                "Age": "1000"
            },
            {
                "Name": "Harry",
                "Age": "17"
            }
        ]
         */
        #endregion

        public static string Get (string key) => Configuration[key];

    }
}