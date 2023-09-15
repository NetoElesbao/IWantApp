using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Infra.Data;
using IWantApp.Models.DTOs;
using IWantApp.Models.Products;
using IWantApp.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryPut
    {
        public static string Pattern => "/categories/{id:guid}";
        public static string[] HttpMethods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action([FromRoute] Guid id, CategoryDTO categoryDTO, ApplicationDbContext context)
        {
            var category = context.Categories.Find(id);

            if (category is null) { return Results.NotFound(); }

            category.EditCategory(categoryDTO.Name);

            if (!category.IsValid) { return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails()); }

            context.SaveChanges();

            return Results.Ok(category); // só pra facilitar a visualização do dev
        }
    }
}