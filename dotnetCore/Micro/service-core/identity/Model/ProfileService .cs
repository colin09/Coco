using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace identity.Model {

    public class ProfileService : IProfileService {

        public async Task GetProfileDataAsync (ProfileDataRequestContext context) {

            try {
                //depending on the scope accessing the user data.
                var claims = context.Subject.Claims.ToList ();

                //set issued claims to return
                context.IssuedClaims = claims.ToList ();
            } catch (Exception ex) {
                //log your error
            }
        }

        public async Task IsActiveAsync (IsActiveContext context) {
            context.IsActive = true;
        }
    }
}

/* 
IdentityServer提供了接口访问用户信息，但是默认返回的数据只有sub，就是上面设置的subject: context.UserName，
要返回更多的信息，需要实现IProfileService接口：

先执行ResourceOwnerPasswordValidator 里的ValidateAsync，
然后执行ProfileService 里的IsActiveAsync，GetProfileDataAsync。


*/