
using System.Security.Claims;

namespace IWantApp.Endpoints.Clients
{
    public class ClientGet
    {

        public static string Pattern => "/clients";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(HttpContext httpContext)
        {
            var userId = httpContext.User;
            var result = new
            {
                Id = userId.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value,
                Name = userId.Claims.FirstOrDefault(e => e.Type == "Name").Value,
                Cpf = userId.Claims.FirstOrDefault(e => e.Type == "Cpf").Value
            };

            return Results.Ok(result);
        }

    }
}