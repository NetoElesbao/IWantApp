


using System.Security.Claims;
using IWantApp.Models.DTOs.Employee;
using IWantApp.Services;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesPost
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handler => Action;

        public static async Task<IResult> Action(
            EmployeeRequestDTO employeeDTO, HttpContext http, UserCreator userCreator)
        {
            var userId = http.User.Claims.First(e => e.Type.Equals(ClaimTypes.NameIdentifier)).Value;


            var ClaimsList = new List<Claim>()
            {
                new ("Name", employeeDTO.Name),
                new ("EmployeeCode", employeeDTO.EmployeeCode),
                new ("CreatedBy", userId)
            };

            (IdentityResult result, string id) = await userCreator.Create(employeeDTO.Email, employeeDTO.Password, ClaimsList);

            if (!result.Succeeded) return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

            return Results.Created("/clients", id);


        }
    }
}