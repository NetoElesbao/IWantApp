


using IWantApp.Models.DTOs;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryDelete
    {
        public static string Pattern => "/categories/{id}";
        public static string[] HttpMethods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(Guid id, ApplicationDbContext context)
        {
            var category = await context.Categories.FindAsync(id);

            if (category is null) { return Results.NotFound(); }

            context.Categories.Remove(category);

            await context.SaveChangesAsync();

            return Results.Ok(new CategoryDTO(category.Id, category.Name));
        }

    }
}