



using IWantApp.Models.DTOs.Product;

namespace IWantApp.Endpoints.Products
{
    public class ProductGetShowcase
    {
        public static string Pattern => "/products/showcase";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(
            ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "name")
        {
            if (row > 10) return Results.Problem("Row with max 10", statusCode: 400);

            var queryBase = context.Products.AsNoTracking().Include(e => e.Category)
            .Where(e => e.Active && e.Category.Active);

            if (orderBy.Equals("name"))
                queryBase = queryBase.OrderBy(e => e.Name);
            else if (orderBy.Equals("price"))
                queryBase = queryBase.OrderBy(e => e.Price);
            else
                return Results.Problem("Order only by price or name");

            var query = queryBase.Skip((page - 1) * row).Take(row).ToList();

            return Results.Ok(query
            .Select(e => new ProductResponseDTO(
                e.Id ,e.Name, e.Category.Name, e.Description, e.HasStock, e.Price, e.Active)));
        }
    }
}