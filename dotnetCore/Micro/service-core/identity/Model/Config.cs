using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace identity.Model {

    public class Config {

        private readonly IConfiguration _configuration;

        public Config (IConfiguration configuration) {
            _configuration = configuration;
        }

        public IEnumerable<IdentityResource> GetIdentityResources () {
            return new List<IdentityResource> {
                new IdentityResources.OpenId (),
                new IdentityResources.Profile (),
            };
        }

        public IEnumerable<ApiResource> GetApiResources () {
            return new List<ApiResource> {
                new ApiResource("api1", "My API"){
                    ApiSecrets = { new Secret ("secret".Sha256 ()) },
                        UserClaims = new List<string> { "role" },
                        },
            };
        }

        public IEnumerable<ApiResource> GetApiResources2 () {
            return new List<ApiResource> {
                new ApiResource("api1", "My API")
            };
        }

        public IEnumerable<Client> GetClients () {
            var accessTokenLifetime = int.Parse (_configuration.GetConnectionString ("AccessTokenLifetime"));
            return new List<Client> {
                new Client {
                    ClientId = "client.reference",
                    ClientSecrets = { new Secret ("A30E6E57-086C-43BE-AF79-67ADECDA0A5B".Sha256 ()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = accessTokenLifetime,
                    AllowedScopes = { "api1" },
                    AccessTokenType = AccessTokenType.Reference
                },
                new Client {
                    ClientId = "client.jwt",
                    ClientName = "jwt client (api)",
                    ClientSecrets = { new Secret ("AB2DC090-0125-4FB8-902A-34AFB64B7D9B".Sha256 ()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = accessTokenLifetime,
                    AllowedScopes = { "api1","email" },
                    AccessTokenType = AccessTokenType.Jwt
                }
            };
        }

    }
}