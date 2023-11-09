// using System.Security.Claims;
// using IWantApp.Models.DTOs.Order;



// namespace IWantApp.Endpoints.Clients
// {
//     public class OrderPost
//     {
//         public static string Pattern => "/orders";
//         public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
//         public static Delegate Handler => Action;

//         [AllowAnonymous]
//         public static async Task<IResult> Action(OrderDTO orderDTO, HttpContext httpContext, ApplicationDbContext context)
//         {
//             var ClientId = httpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);

//             //incompleto, parei aqui 4:40
//         }
//     }


// }
