using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IWantApp.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IWantApp.Endpoints.Security
{
    public class TokenPost
    {
        public static string Pattern => "/token";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action(LoginRequest login, UserManager<IdentityUser> manager)
        {
            var user = manager.FindByEmailAsync(login.Email).Result;
            if (user.Equals(null)) Results.BadRequest();
            if (!manager.CheckPasswordAsync(user, login.Password).Result) Results.BadRequest();



            var key = Encoding.ASCII.GetBytes("AS#@$7gfnF*e¨7$fkrgg");
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, login.Email)
                }),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Audience = "IWantApp",
                Issuer = "Issuer"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Results.Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}