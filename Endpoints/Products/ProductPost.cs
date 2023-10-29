using System.Security.Claims;
using IWantApp.Models.DTOs;
using IWantApp.Utilities;


namespace IWantApp.Endpoints.Products
{
    public class ProductPost
    {
        public static string Pattern => "/products";
        public static string[] HttpMethods = new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(ProductDTO productDTO, HttpContext http, ApplicationDbContext context)
        {
            var UserId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var product = new Product
            (productDTO.Name, productDTO.CategoryId, productDTO.Description, productDTO.HasStock, UserId, UserId);

            if (!product.IsValid) return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return Results.Created("/products", product.Id);
        }

    }
}
