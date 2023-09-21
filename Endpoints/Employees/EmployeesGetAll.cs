



using IWantApp.Utilities;
using Dapper;
using IWantApp.Models.DTOs.Employees;
using Microsoft.Data.SqlClient;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesGetAll
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;


        //ENDPOINT DE SOLICITAÇÃO DE USUARIOS USANDO O DAPPER, COM PAGINAÇÃO
        public static IResult Action(QueryAllUsersWithClaimName service, int? page = 1, int? rows = 10)
        {
            return Results.Ok(service.ExecuteQuery(page.Value, rows.Value));
        }

    }
}
      
      
      
      
      
      
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
