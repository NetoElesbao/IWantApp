



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

        //ENDPOINT DE SOLICITAÇÃO DE USUARIOS USANDO O ENTITY, COM PAGINAÇÃO
        // public static IResult Action(int page, int rows, UserManager<IdentityUser> manager)
        // {
        //     var users = manager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        //     var usersDTO = new List<EmployeeResponseDTO>();

        //     foreach (var user in users)
        //     {
        //         var claims = manager.GetClaimsAsync(user).Result;
        //         var claim = claims.FirstOrDefault(e => e.Type.Equals("Name"));
        //         var claimValidation = claim != null ? claim.Value : string.Empty;
        //         if (claimValidation.Any()) { usersDTO.Add(new EmployeeResponseDTO(user.Email, claimValidation)); }
        //     }

        //     return Results.Ok(usersDTO.OrderBy(e => e.Name));
        // }

        //ENDPOINT DE SOLICITAÇÃO DE USUARIOS USANDO O DAPPER, COM PAGINAÇÃO
        public static IResult Action(int page, int rows, IConfiguration configuration)
        {
            var databse = new SqlConnection(configuration.GetConnectionString("Connection"));

            var query = databse.Query
            (
                @"select Email as 'Emails', ClaimValue as 'Names'
                from AspNetUsers a inner join AspNetUserClaims b
                on a.Id = b.UserId and ClaimType = 'Name' 
                order by Names
                offset (@page - 1) * @rows rows fetch next @rows rows only ",
                new {page, rows}
            );
            return Results.Ok(query);
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