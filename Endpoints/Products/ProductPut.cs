using System.Security.Claims;
using IWantApp.Models.DTOs.Product;
using IWantApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Products
{
    public class ProductPut
    {
        public static string Pattern => "/products/{id:guid}";
        public static string[] HttpMethods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action
        ([FromRoute] Guid id, HttpContext http, ProductRequestDTO productDTO, [FromServices] ApplicationDbContext context)
        {
            var UserId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var product = await context.Products.FindAsync(id);

            if (product is null) { return Results.NotFound(); }

            product.EditProduct(productDTO.Name, productDTO.CategoryId, productDTO.Description, productDTO.HasStock, UserId);

            if (!product.IsValid) { return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails()); }

            await context.SaveChangesAsync();

            return Results.Ok(product.Id); // só pra facilitar a visualização do dev
        }
    }
}