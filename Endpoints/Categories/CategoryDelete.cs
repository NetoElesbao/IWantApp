using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
namespace IWantApp.Endpoints.Categories
{
    public class CategoryDelete
    {
        public static string Pattern => "/categories/{id}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action([FromRoute]Guid id, ApplicationDbContext context)
        {
            context.Categories.Remove(context.Categories.FirstOrDefault(c => c.Id.Equals(id)));
            context.SaveChanges();
            return Results.Ok();
        }
    }
}
