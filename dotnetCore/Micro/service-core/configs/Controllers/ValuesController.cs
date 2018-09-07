using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using configs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace configs.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        private IConfigurationRoot Config { get; set; }
        private IOptionsSnapshot<ConfigData> IConfigServerData { get; set; }
        private ConfigServerClientSettingsOptions ConfigServerClientSettingsOptions { get; set; }

        public ValuesController (IConfigurationRoot config, IOptionsSnapshot<ConfigData> configServerData, IOptions<ConfigServerClientSettingsOptions> confgServerSettings) {
            // The ASP.NET DI mechanism injects the data retrieved from the Spring Cloud Config Server 
            // as an IOptionsSnapshot<ConfigData>. This happens because we added the call to:
            // "services.Configure<ConfigData>(Configuration);" in the StartUp class
            if (configServerData != null)
                IConfigServerData = configServerData;

            // The settings used in communicating with the Spring Cloud Config Server
            if (confgServerSettings != null)
                ConfigServerClientSettingsOptions = confgServerSettings.Value;

            Config = config;
        }

        // GET api/values
        [HttpGet ("config")]
        public ConfigData GetConfig () {
            return  IConfigServerData.Value;
        }

        [HttpGet ("options")]
        public ConfigServerClientSettingsOptions GetOptions () {
            return ConfigServerClientSettingsOptions;
        }

        [HttpGet ("reload")]
        public IConfigurationRoot Reload () {
            Config.Reload();           
            //return  "Config.Reload !";
            return Config;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public ActionResult<string> Get (int id) {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}