using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IWantApp.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IWantApp.Endpoints.Security
{
    public class TokenPost
    {
        public static string Pattern => "/token";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [AllowAnonymous]
        public static IResult Action(LoginRequest loginRequest, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

            if (user == null) return Results.NotFound("User not found!");

            if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result) return Results.BadRequest("Bad password!");

            var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email, loginRequest.Email)
                }),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Audience = configuration["JwtBearerTokenSettings:Audience"],

                Issuer = configuration["JwtBearerTokenSettings:Issuer"],

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Results.Ok(new { token = tokenHandler.WriteToken(token) });

        }
    }
}


