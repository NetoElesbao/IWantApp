


using System.Security.Claims;
using IWantApp.Models.DTOs.Client;
using IWantApp.Utilities;

namespace IWantApp.Endpoints.Clients
{
    public class ClientPost
    {
        public static string Pattern => "/clients";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(ClientDTO client, UserManager<IdentityUser> userManager)
        {
            // var userId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var newUser = new IdentityUser { UserName = client.Email, Email = client.Email };
            var result = await userManager.CreateAsync(newUser, client.Password);
            if (!result.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            var ClaimsList = new List<Claim>()
            {
                new ("Name", client.Name),
                new ("Cpf", client.Cpf)
            };
            var resultClaims = await userManager.AddClaimsAsync(newUser, ClaimsList);
            if (!resultClaims.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            return Results.Created("/clients", newUser.Id);

        }
    }
}