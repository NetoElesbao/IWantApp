



using IWantApp.Utilities;
using Dapper;
using IWantApp.Models.DTOs.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeesGetAll
    {
        public static string Pattern => "/employees";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;


        //ENDPOINT DE SOLICITAÇÃO DE USUARIOS USANDO O DAPPER, COM PAGINAÇÃO
        public static IResult Action(IConfiguration configuration, int? page = 1, int? rows = 10)
        {
            var databse = new SqlConnection(configuration.GetConnectionString("Connection"));

            var query = @"select Email, ClaimValue as Name
                        from AspNetUsers a inner join AspNetUserClaims b
                        on a.Id = b.UserId and ClaimType = 'Name' 
                        order by Name
                        offset (@page - 1) * @rows rows fetch next @rows rows only ";

            var employees = databse.Query<EmployeeResponseDTO>
            (
                query,
                new { page, rows }
            );

            return Results.Ok(employees);
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
    }
}