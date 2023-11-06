using IWantApp.Models.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Products
{
    public class ProductGet
    {
        public static string Pattern => "/products/{id:guid}";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
        {
            var product = context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id.Equals(id)).Result;
            if (product is null) return Results.NotFound("Product not found!");
            var result = new ProductResponseDTO(
                product.Name, product.Category.Name, product.Description, product.HasStock, product.Price, product.Active);
            return Results.Ok(result);
        }
    }
}