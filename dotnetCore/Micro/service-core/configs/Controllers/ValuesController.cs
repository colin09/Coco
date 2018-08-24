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

        /*
        private readonly IConfigurationRoot _config;
        private readonly IOptionsSnapshot<ConfigManager> _configDemo;

        public ValuesController (IConfigurationRoot config, IOptionsSnapshot<ConfigManager> configDemo) {
            _config = config;
            _configDemo = configDemo;
        }

        // GET api/values
        [HttpGet("config")]
        public ConfigManager GetConfig () {
           //两种方式获取配置文件的数据
            //var demo = new Demo
            //{
            //    Name = _config["name"],
            //    Age = int.Parse(_config["age"]),
            //    Env = _config["env"]
            //};
            var config = _configDemo.Value;
            return config;
        }*/

        private IConfigurationRoot Config { get; set; }
        private IOptionsSnapshot<ConfigServerData> IConfigServerData { get; set; }
        private ConfigServerClientSettingsOptions ConfigServerClientSettingsOptions { get; set; }

        public ValuesController (IConfigurationRoot config, IOptionsSnapshot<ConfigServerData> configServerData, IOptions<ConfigServerClientSettingsOptions> confgServerSettings) {
            // The ASP.NET DI mechanism injects the data retrieved from the Spring Cloud Config Server 
            // as an IOptionsSnapshot<ConfigServerData>. This happens because we added the call to:
            // "services.Configure<ConfigServerData>(Configuration);" in the StartUp class
            if (configServerData != null)
                IConfigServerData = configServerData;

            // The settings used in communicating with the Spring Cloud Config Server
            if (confgServerSettings != null)
                ConfigServerClientSettingsOptions = confgServerSettings.Value;

            Config = config;
        }

        // GET api/values
        [HttpGet ("config")]
        public ConfigServerData GetConfig () {
            return  IConfigServerData.Value;
        }

        [HttpGet ("options")]
        public ConfigServerClientSettingsOptions GetOptions () {
            return ConfigServerClientSettingsOptions;
        }

        [HttpGet ("reload")]
        public string Reload () {
            Config.Reload();           
            return  "Config.Reload !";
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