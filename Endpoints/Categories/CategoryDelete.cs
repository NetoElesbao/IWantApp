


using IWantApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryDelete
    {
        public static string Pattern => "/categories/{id}";
        public static string[] HttpMethods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handler => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(Guid id, [FromServices] ApplicationDbContext context)
        {
            var category = await context.Categories.FindAsync(id);

            if (category is null) { return Results.NotFound(); }

            context.Categories.Remove(category);

            await context.SaveChangesAsync();

            return Results.Ok(new CategoryDTO(category.Id, category.Name));
        }

    }
}