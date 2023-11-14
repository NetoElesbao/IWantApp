using System.Security.Claims;
using IWantApp.Models.DTOs.Order;
using IWantApp.Models.Orders;



namespace IWantApp.Endpoints.Clients
{
    public class OrderPost
    {
        public static string Pattern => "/orders";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;


        public static async Task<IResult> Action(OrderDTO orderDTO, HttpContext httpContext, ApplicationDbContext context)
        {
            var userId = httpContext.User.Claims.First(e => e.Type == ClaimTypes.NameIdentifier).Value;
            var userName = httpContext.User.Claims.First(e => e.Type == "Name").Value;

            var products = new List<Product>();
            var productsFound = context.Products.Where(e => orderDTO.ProductsIds.Contains(e.Id)).ToList();

            var order = new Order(userId, userName, productsFound, orderDTO.DeliveryAddress);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return Results.Created($"orders/{order.Id}", order.Id);
        }
    }


}