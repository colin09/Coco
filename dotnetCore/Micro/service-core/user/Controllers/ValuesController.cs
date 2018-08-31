using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Client;
using user.Model;
using Microsoft.Extensions.Logging;

namespace user.Controllers {
    [Route ("/")]
    [ApiController]
    public class ValuesController : ControllerBase {

        private const string IdentityApplicationName = "identity";
        private readonly DiscoveryHttpClientHandler _handler;
        private readonly IConfiguration _configuration;
       private readonly ILogger _logger;

        public ValuesController (IDiscoveryClient client, IConfiguration configuration,ILogger<ValuesController> logger) {
            _configuration = configuration;
            _handler = new DiscoveryHttpClientHandler (client);
            _logger = logger;
        }

        [HttpPost("get")] 
        public ActionResult<IEnumerable<string>> Get([FromBody] LoginRequest input)
        {  
            _logger.LogInformation ("####################################");
            
            var discoveryClient = new DiscoveryClient ($"http://{IdentityApplicationName}", _handler) {
                Policy = new DiscoveryPolicy { RequireHttps = false }
            };
          

            return new string[] { "project", "user", $"{input.Name}:{input.Password}" };
        }

        [HttpGet ("search")]
        public IActionResult Get (string name, string password) {
            var account = Account.GetAll ().FirstOrDefault (x => x.Name == name && x.Password == password);
            if (account != null) {
                return Ok (account);
            } else {
                return NotFound ();
            }
        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login ([FromBody] LoginRequest input) {

             _logger.LogInformation ("####################################");

            var discoveryClient = new DiscoveryClient ($"http://{IdentityApplicationName}", _handler) {
                Policy = new DiscoveryPolicy { RequireHttps = false }
            };
            var disco = await discoveryClient.GetAsync ();
            if (disco.IsError) {
                 _logger.LogInformation ($" ==> discoveryClient:{IdentityApplicationName} error, {disco.Error}");
                throw new Exception (disco.Error);
            }
            _logger.LogInformation ($" ==> discoveryClient:{IdentityApplicationName} success, {disco}");

            var clientId = _configuration.GetSection ("IdentityServer:ClientId").Value;
            if (string.IsNullOrEmpty (clientId)) 
                throw new Exception ("clientId is not value.");

            var clientSecrets = _configuration.GetSection ("IdentityServer:ClientSecrets").Value;
            if (string.IsNullOrEmpty (clientSecrets)) 
                throw new Exception ("clientSecrets is not value.");

            var tokenClient = new TokenClient (disco.TokenEndpoint, clientId, clientSecrets, _handler);
            //var response = await tokenClient.RequestResourceOwnerPasswordAsync (input.Name, input.Password, "api1 offline_access"); //如果需要刷新token那么这里要多传递一个offline_access参数，不传的话RefreshToken为null
            var response = await tokenClient.RequestResourceOwnerPasswordAsync (input.Name, input.Password, "api1");
            if (response.IsError) 
                throw new Exception (response.Error);
                
            return Ok (new {
                AccessToken = response.AccessToken,
                ExpireIn = response.ExpiresIn,
                RefreshToken = response.RefreshToken
            });
        }

    }
}