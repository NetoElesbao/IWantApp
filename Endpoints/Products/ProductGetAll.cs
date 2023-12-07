using IWantApp.Models.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Products
{
    public class ProductGetAll
    {
        public static string Pattern => "/products";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        [Authorize(policy: "EmployeePolicy")]
        public static async Task<IResult> Action([FromServices] ApplicationDbContext context)
        {
            var productsOrdenaded = context.Products.AsNoTracking().Include(p => p.Category).OrderBy(p => p.Name);
            var result = productsOrdenaded.Select(p =>
            new ProductResponseDTO(p.Id, p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));
            return Results.Ok(result);
        }
    }
}