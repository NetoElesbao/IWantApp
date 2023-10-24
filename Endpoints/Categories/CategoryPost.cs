using System.Security.Claims;
using IWantApp.Infra.Data;
using IWantApp.Models.DTOs;
using IWantApp.Models.Products;
using IWantApp.Utilities;
using Microsoft.AspNetCore.Authorization;


namespace IWantApp.Endpoints.Categories
{
    public class CategoryPost
    {
        public static string Pattern => "/categories";
        public static string[] HttpMethods = new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(CategoryDTO categoryDTO, HttpContext http, ApplicationDbContext context)
        {
            var UserId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var category = new Category(categoryDTO.Name, UserId, UserId);

            if (!category.IsValid)
            {
                return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
            }

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return Results.Created("/categories", category.Id);
        }

    }
}
