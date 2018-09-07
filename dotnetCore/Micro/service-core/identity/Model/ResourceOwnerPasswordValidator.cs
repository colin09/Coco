using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using identity.Common;
using IdentityModel;
using IdentityServer4.Validation;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Client;

namespace identity.Model {

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator {

        private readonly DiscoveryHttpClientHandler _handler;
        private const string UserApplicationName = "user";

        public ResourceOwnerPasswordValidator (IDiscoveryClient client) {
            _handler = new DiscoveryHttpClientHandler (client);
        }

        public async Task ValidateAsync (ResourceOwnerPasswordValidationContext context) {
            //调用用户中心的验证用户名密码接口
            var client = new HttpClient (_handler);

            var url = $"http://{UserApplicationName}/search?name={context.UserName}&password={context.Password}";
            var result = await client.GetAsync (url);
            if (result.IsSuccessStatusCode) {
                var user = await result.Content.ReadAsObjectAsync<dynamic> ();
                var claims = new List<Claim> () { new Claim ("role", user.role.ToString ()) };
                context.Result = new GrantValidationResult (user.id.ToString (), OidcConstants.AuthenticationMethods.Password, claims);
            } else {
                context.Result = new  GrantValidationResult(null);
            }
        }
    }
}