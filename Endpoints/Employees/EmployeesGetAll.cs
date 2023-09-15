



using Dapper;
using IWantApp.Models.DTOs.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesGetAll
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;


        public static IResult Action(int page, int rows, UserManager<IdentityUser> manager)
        {
            var users = manager.Users.Skip((page - 1) * rows).Take(rows).ToList();
            var usersDTO = new List<EmployeeResponseDTO>();

            foreach (var user in users)
            {
                var claims = manager.GetClaimsAsync(user).Result;
                var claim = claims.FirstOrDefault(e => e.Type.Equals("Name"));
                var result = claim != null ? claim.Value : string.Empty;
                usersDTO.Add(new EmployeeResponseDTO(user.Email, result));
            }
            return Results.Ok(usersDTO);
        }

        public static IResult Action(int page, int rows, IConfiguration configuration)
        {
            var database = new SqlConnection(configuration.GetConnectionString("Connection"));
            var users = database.Query<EmployeeResponseDTO>
            (
                @"select Email, ClaimValue as Name 
                  from AspNetUsers a inner join AspNetUserClaims b
                  on a.Id = b.UserId and ClaimType = 'Name'
                  order by Name
                  OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY",
                  new { page, rows }

            );

            return Results.Ok(users);
        }
    }
}





















// public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
// {
//     var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
//     var usersDTO = new List<EmployeeResponseDTO>();

//     foreach (var user in users)
//     {
//         var claimsUsers = userManager.GetClaimsAsync(user).Result;
//         var claimUser = claimsUsers.FirstOrDefault(e => e.Type.Equals("Name"));
//         var claim = claimUser != null ? claimUser.Value : string.Empty;

//         usersDTO.Add(new EmployeeResponseDTO(user.Email, claim));
//     }

//     return Results.Ok(usersDTO);
// }