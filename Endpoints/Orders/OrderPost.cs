using System.Security.Claims;
using IWantApp.Models.DTOs.Order;
using IWantApp.Models.Orders;
using IWantApp.Services;
using Microsoft.AspNetCore.Mvc;



namespace IWantApp.Endpoints.Clients
{
    public class OrderPost
    {
        public static string Pattern => "/orders";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [Authorize(Policy = "CpfPolicy")]
        public static async Task<IResult> Action(OrderRequestDTO orderDTO, HttpContext httpContext, [FromServices] ApplicationDbContext context)
        {
            var userId = httpContext.User.Claims.First(e => e.Type == ClaimTypes.NameIdentifier).Value;
            var userName = httpContext.User.Claims.First(e => e.Type == "Name").Value;

            List<Product> productsFound = null;

            if (orderDTO.ProductsIds is not null || orderDTO.ProductsIds.Any())
                productsFound = context.Products.Where(e => orderDTO.ProductsIds.Contains(e.Id)).ToList();

            var order = new Order(userId, userName, productsFound, orderDTO.DeliveryAddress);

            if (!order.IsValid) return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return Results.Created($"orders/{order.Id}", order.Id);
        }
    }


}
