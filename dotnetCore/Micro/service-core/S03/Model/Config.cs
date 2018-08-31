using System.Collections;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace S03.Model
{
    public class Config
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>{
                new TestUser{
                    SubjectId="1",
                    Username="kk",
                    Password="123456"
                }
            };
        }

        //所有可以访问的Resource
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","My Api")
            };
        }

        //客户端
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId="client",
                    AllowedGrantTypes= GrantTypes.ClientCredentials,//模式：最简单的模式
                    ClientSecrets={//私钥
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={//可以访问的Resource
                        "api"
                    }
                },
                new Client()
                {
                    ClientId="client.pwd",
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,//模式：用户密码模式
                    RequireClientSecret= false, //不需要密码
                    ClientSecrets={//认证私钥
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={//可以访问的Resource
                        "api"
                    }
                },
            };
        }
    }
}