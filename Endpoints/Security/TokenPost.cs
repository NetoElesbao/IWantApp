using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IWantApp.Models.Authentication;

namespace IWantApp.Endpoints.Security
{
    public class TokenPost
    {
        public static string Pattern => "/token";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action
        (
            LoginRequest loginRequest,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            ILogger<TokenPost> logger,
            IWebHostEnvironment environment
        )
        {
            logger.LogInformation("Getting token");
            logger.LogWarning("Warning");
            logger.LogError("Error");

            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            var userCLaims = await userManager.GetClaimsAsync(user);

            if (user is null) return Results.NotFound("User not found!");

            if (!await userManager.CheckPasswordAsync(user, loginRequest.Password)) return Results.BadRequest("Bad password!");

            var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);

            var subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Email, loginRequest.Email),
                    new (ClaimTypes.NameIdentifier, user.Id)
                });
            subject.AddClaims(userCLaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Issuer = configuration["JwtBearerTokenSettings:Issuer"],

                Audience = configuration["JwtBearerTokenSettings:Audience"],

                Expires = environment.IsDevelopment() || environment.IsStaging() ?
                        DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(2)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Results.Ok(new { token = tokenHandler.WriteToken(token) });

        }
    }
}