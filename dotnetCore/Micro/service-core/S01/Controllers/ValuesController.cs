using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Client;
using IdentityModel.Client;

namespace S01.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        private readonly DiscoveryHttpClientHandler _handler;
        private const string ProductUrl = "http://S02/api/values";

        public ValuesController (IDiscoveryClient client, ILoggerFactory logFactory) {
            if (client == null)
                System.Console.WriteLine ("IDiscoveryClient is null.");
            else
                System.Console.WriteLine (client);
            _handler = new DiscoveryHttpClientHandler (client);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            return new string[] { "value1", "S01" };
        }

        // GET api/values/S02
        [HttpGet ("S02")]
        public async Task<string> GoProductAsync () {
            var client = new HttpClient (_handler, false);
            return await client.GetStringAsync (ProductUrl);
        }

        // GET api/values/S2
        [HttpGet ("S2")]
        public async Task<DiscoveryResponse> Get (int id) {

            var discoveryClient = new DiscoveryClient ($"http://S02", _handler) {
                Policy = new DiscoveryPolicy { RequireHttps = false }
            };
            return await discoveryClient.GetAsync ();
            //return "value";
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