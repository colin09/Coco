using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace order.Controllers {

    [Route ("/")]
    [ApiController]
    public class ValuesController : ControllerBase {

        private readonly ILogger _logger;

        public ValuesController (ILogger<ValuesController> logger) {
            _logger = logger;
        }

        // admin role
        [HttpGet ("admin")]
        [Authorize (Roles = "admin")]
        public IActionResult Get1 () {
            //_logger.LogInformation(JsonConvert.SerializeObject(User));
            var userId = User.Claims.FirstOrDefault (x => x.Type == "sub")?.Value;
            var role = User.Claims.FirstOrDefault (x => x.Type == "role")?.Value;
            return Ok (new { userId, role });
        }
        // normal role
        [HttpGet ("normal")]
        [Authorize (Roles = "normal")]
        public IActionResult Get2 () {
            var userId = User.Claims.FirstOrDefault (x => x.Type == "sub")?.Value;
            return Ok (new { role = "normal", userId = userId });
        }
        // any role
        [HttpGet ("any")]
        [Authorize]
        public IActionResult Get3 () {
            var userId = User.Claims.FirstOrDefault (x => x.Type == "sub")?.Value;
            return Ok (new { role = "any", userId = userId });
        }
        // Anonymous
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get () {
            return Ok (new { role = "allowAnonymous" });
        }
    }
}