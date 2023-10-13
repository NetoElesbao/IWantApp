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

        public static IResult Action(CategoryDTO categoryDTO, ApplicationDbContext context)
        {
            var category = new Category(categoryDTO.Name, "nome teste", "nome teste");

            if (!category.IsValid)
            {
                return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
            }

            context.Categories.Add(category);
            context.SaveChanges();

            return Results.Created("/categories", category.Id);
        }

    }
}
 