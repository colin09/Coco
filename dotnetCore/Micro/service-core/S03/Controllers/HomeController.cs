using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;

namespace S03.Controllers {

    [Route ("/")]
    [ApiController]
    public class HomeController : ControllerBase {



        
        /*
        [HttpPost ("authenticate")]
        public IActionResult Authenticate ([FromBody] string name, string pwd) {
            var user = new UserDTO { Id = DateTime.UtcNow.ToString (), Name = name, Password = pwd };

            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (Consts.Secret);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays (1);

            var tokenDecriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (new Claim[] {
                new Claim (JwtClaimTypes.Audience, "api"),
                new Claim (JwtClaimTypes.Issuer, "http://localhost:5200"),
                new Claim (JwtClaimTypes.Id, user.Id),
                new Claim (JwtClaimTypes.Name, user.Name),
                new Claim (JwtClaimTypes.Email, user.Name),
                new Claim (JwtClaimTypes.PhoneNumber, user.Name)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken (tokenDecriptor);
            var tokenString = tokenHandler.WriteToken (token);

            return Ok (new {
                access_token = tokenString,
                    token_type = "Bearer",
                    profile = new {
                        sid = user.Id,
                            name = user.Name,
                            auth_time = new DateTimeOffset (authTime).ToUnixTimeSeconds (),
                            expires_at = new DateTimeOffset (expiresAt).ToUnixTimeSeconds ()
                    }
            });
        }*/

    }

    public class UserDTO {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
    }

    public class Consts {

        public string Secret => "asdfghjkl123456";
    }

}