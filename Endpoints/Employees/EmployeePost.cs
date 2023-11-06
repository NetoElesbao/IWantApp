


using System.Security.Claims;
using IWantApp.Models.DTOs.Employee;
using IWantApp.Utilities;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesPost
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(EmployeeRequestDTO employeeDTO, HttpContext http, UserManager<IdentityUser> userManager)
        {
            var userId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var newUser = new IdentityUser { UserName = employeeDTO.Email, Email = employeeDTO.Email };
            var result = await userManager.CreateAsync(newUser, employeeDTO.Password);
            if (!result.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            var ClaimsList = new List<Claim>()
            {
                new ("Name", employeeDTO.Name),
                new ("EmployeeCode", employeeDTO.EmployeeCode),
                new ("CreatedBy", userId)
            };
            var resultClaims = await userManager.AddClaimsAsync(newUser, ClaimsList);
            if (!resultClaims.Succeeded) { return Results.ValidationProblem(result.Errors.ConvertToProblemDetails()); }

            return Results.Created("/employees", newUser.Id);

        }
    }
}