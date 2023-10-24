using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Infra.Data;
using IWantApp.Models.DTOs;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryGetAll
    {
        public static string Pattern => "/categories";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(ApplicationDbContext context)
        {
            var users = context.Categories.Select(c => new CategoryDTO(c.Id, c.Name)).ToList();

            return Results.Ok(users); 
        }
    }
} 