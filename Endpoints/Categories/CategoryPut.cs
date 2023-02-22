using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
namespace IWantApp.Endpoints.Categories
{
    public class CategoryPut
    {
        public static string Pattern => "categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethods.Put.ToString() };
        public static Delegate Handler = Action;

        public static IResult Action([FromRoute] Guid id, CategoryRequest request, ApplicationDbContext context)
        {
            var category = context.Categories.Where(c => c.Id.Equals(id)).FirstOrDefault();
            category.Name = request.Name;
            category.Active = request.Active;

            context.SaveChanges();

            return Results.Ok();
        }
    }
}
