using Dapper;
using IWantApp.Models.DTOs.Employee;

namespace IWantApp.Infra.Data
{
    public class QueryAllUsersWithClaimName
    {
        private readonly IConfiguration _configuration;
        public QueryAllUsersWithClaimName(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> ExecuteQuery(int page, int rows)
        {
            var database = new SqlConnection(_configuration.GetConnectionString("Connection"));

            var query = @"select Email, ClaimValue as Name
                        from AspNetUsers a inner join AspNetUserClaims b
                        on a.Id = b.UserId and ClaimType = 'Name'
                        order by Name
                        offset (@page - 1) * @rows rows fetch next @rows rows only";

            return await database.QueryAsync<EmployeeResponseDTO>(query, new { page, rows });
        }
    }
}