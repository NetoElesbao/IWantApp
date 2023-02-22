using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Pattern => "/categories";
    public static string[] Methods => new string[] { HttpMethods.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.Name, "testNameCreatedBy", "testNameEditedBy");

        if (!category.IsValid)
            return Results.BadRequest(category.Notifications);

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
