using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantApp.Infra.Data;
using IWantApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories
{
    public class CategoryGetAll
    {
        public static string Pattern => "/categories";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action([FromServices] ApplicationDbContext context)
        {
            var categories = context.Categories.AsNoTracking().Select(c =>
            new CategoryDTO(c.Id, c.Name)).ToList();

            return Results.Ok(categories);
        }
    }
}