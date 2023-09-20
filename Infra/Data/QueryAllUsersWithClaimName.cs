

using Dapper;
using IWantApp.Models.DTOs.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private readonly IConfiguration configuration;
        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<EmployeeResponseDTO> ExecuteQuery(int page, int rows)
        {
            var database = new SqlConnection(configuration.GetConnectionString("Connection"));

            var query =
            @"select Email, ClaimValue as Name
            from AspNetUsers a inner join AspNetUserClaims b
            on a.Id = b.UserId and ClaimType = 'Name' 
            order by Name
            offset (@page - 1) * @rows rows fetch next @rows rows only ";

            return database.Query<EmployeeResponseDTO>( query, new {page, rows});
        }

    }
}