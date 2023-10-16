


using System.Security.Claims;
using IWantApp.Models.DTOs.Employees;
using IWantApp.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesPost
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action(EmployeeRequestDTO employeeDTO, HttpContext http, UserManager<IdentityUser> userManager)
        {
            var userId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var newUser = new IdentityUser { UserName = employeeDTO.Email, Email = employeeDTO.Email };
            var result = userManager.CreateAsync(newUser, employeeDTO.Password).Result;
            if (!result.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            var ClaimsList = new List<Claim>()
            {
                new ("Name", employeeDTO.Name),
                new ("EmployeeCode", employeeDTO.EmployeeCode),
                new ("CreatedBy", userId)
            };
            var resultClaims = userManager.AddClaimsAsync(newUser, ClaimsList).Result;
            if (!resultClaims.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            return Results.Created("/employees", newUser.Id);

        }
    }
}