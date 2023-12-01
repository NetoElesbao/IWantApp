using System.Security.Claims;
using IWantApp.Models.DTOs;
using IWantApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryPut
    {
        public static string Pattern => "/categories/{id:guid}";
        public static string[] HttpMethods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handler => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action([FromRoute] Guid id, HttpContext http, CategoryDTO categoryDTO, ApplicationDbContext context)
        {
            var UserId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var category = await context.Categories.FindAsync(id);

            if (category is null) { return Results.NotFound(); }

            category.EditCategory(categoryDTO.Name, UserId);

            if (!category.IsValid) { return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails()); }

            await context.SaveChangesAsync();

            return Results.Ok(category.Id); // só pra facilitar a visualização do dev
        }
    }
}