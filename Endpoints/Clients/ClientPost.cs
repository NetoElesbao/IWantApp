


using System.Security.Claims;
using IWantApp.Models.DTOs.Client;
using IWantApp.Services;

namespace IWantApp.Endpoints.Clients
{
    public class ClientPost
    {
        public static string Pattern => "/clients";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(ClientDTO client, UserCreator userCreator)
        {
            var ClaimsList = new List<Claim>()
            {
                new ("Name", client.Name),
                new ("Cpf", client.Cpf)
            };

            (IdentityResult result, string id) = await userCreator.Create(client.Email, client.Password, ClaimsList);

            if (!result.Succeeded) return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

            return Results.Created("/clients", id);

        }
    }
}