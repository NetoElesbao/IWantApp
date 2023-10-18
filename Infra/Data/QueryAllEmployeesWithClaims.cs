using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IWantApp.Models.DTOs.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data
{
    public class QueryAllEmployeesWithClaims
    {
        private readonly IConfiguration configuration;
        public QueryAllEmployeesWithClaims(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public IEnumerable<EmployeeResponseDTO> ExecuteQuery(int page, int rows)
        {
            var db = new SqlConnection(configuration.GetConnectionString("Connection"));

            var query = db.Query<EmployeeResponseDTO>
            (
                @"select Email, ClaimValue as Names
                        from AspNetUsers a inner join AspNetUserClaims b
                        on a.Id = b.UserId and ClaimType = 'Name'
                        order by Names
                        offset (@page - 1) * @rows rows fetch next @rows rows only",
                new { page, rows }
            );

            return query;
        }
    }
}