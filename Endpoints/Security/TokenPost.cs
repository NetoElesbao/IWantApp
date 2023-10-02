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

        public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager)
        {
            var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

            if (user is null) return Results.NotFound("User not found!");
            if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result) return Results.BadRequest("Bad password!");

            var key = Encoding.ASCII.GetBytes("erg#$Af@$A#F$#reggg5ew4g4t4t");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Email, loginRequest.Email)
                }),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Issuer = "Issuer",

                Audience = "IWantApp"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Results.Ok(new { token = tokenHandler.WriteToken(token) });
        }

    }
}
