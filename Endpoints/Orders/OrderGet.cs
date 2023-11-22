


using System.Security.Claims;
using IWantApp.Models.DTOs.Order;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Orders
{
    public class OrderGet
    {
        public static string Pattern => "/orders/{id:guid}";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;


        public static async Task<IResult> Action(
            [FromRoute] Guid id, HttpContext httpContext, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            var userId = httpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);
            // var userEmail = httpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value;
            var EmployeeClaim = httpContext.User.Claims.FirstOrDefault(e => e.Type == "EmployeeCode").Value;

            var order = await dbContext.Orders.Include(e => e.Products).FirstAsync(e => e.Id == id);
            if (order.ClientId != userId.Value && EmployeeClaim == null)
                return Results.Forbid();
            if (order is null) return Results.NotFound("Order not found!");

            var client = await userManager.FindByIdAsync(order.ClientId);

            var productResponse = order.Products.Select(e => new OrderProduct(e.Id, e.Name)).ToList();
            return Results.Ok(
                new OrderResponseDTO(order.Id, client.Email, productResponse, order.DeliveryAddress));
        }
    }
}



