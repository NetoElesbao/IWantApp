


using System.Security.Claims;
using IWantApp.Models.DTOs.Employees;
using IWantApp.Utilities;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesPost
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action(EmployeeRequestDTO employeeDTO, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser { UserName = employeeDTO.Email, Email = employeeDTO.Email };
            var result = userManager.CreateAsync(user, employeeDTO.Password).Result;
            if (!result.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            // var ClaimsList = new List<Claim>()
            // {
            //     new ("Name", employeeDTO.Name),
            //     new ("EmployeeCode", employeeDTO.EmployeeCode)
            // };
            // var resultClaims = userManager.AddClaimsAsync(user, ClaimsList).Result;
            // if (!resultClaims.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            return Results.Created("/employees", user.Id);

        }
    }
} 