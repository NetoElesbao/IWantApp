using IWantApp.Models.DTOs;

namespace IWantApp.Endpoints.Products
{
    public class ProductGetAll
    {
        public static string Pattern => "/products";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(ApplicationDbContext context)
        {
            var products = context.Products.Select(c => new ProductDTO(c.Name, c.CategoryId, c.Description, c.HasStock)).ToList();

            return Results.Ok(products);
        }
    }
}