using IWantApp.Models.DTOs;


namespace IWantApp.Endpoints.Products
{
    public class ProductDelete
    {
        public static string Pattern => "/products/{id}";
        public static string[] HttpMethods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(Guid id, ApplicationDbContext context)
        {
            var product = await context.Products.FindAsync(id);

            if (product is null) { return Results.NotFound(); }

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return Results.Ok(new ProductDTO(product.Name, product.CategoryId, product.Description, product.HasStock));
        }

    }
}