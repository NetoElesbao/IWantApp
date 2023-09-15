using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Infra.Data;
using IWantApp.Models.DTOs;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryDelete
    {
        public static string Pattern => "/categories/{id}";
        public static string[] HttpMethods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action(Guid id, ApplicationDbContext context)
        {
            var category = context.Categories.Find(id);

            if (category is null) { return Results.NotFound(); }

            context.Categories.Remove(category);

            context.SaveChanges();

            return Results.Ok(new CategoryDTO(category.Id, category.Name));
        }

    }
}