using Dapper;
using IWantApp.Models.DTOs.Product;

namespace IWantApp.Infra.Data
{
    public class QueryallproductsSold
    {
        private readonly IConfiguration Configuration;
        public QueryallproductsSold(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IEnumerable<ProductSoldResponse>> ExecuteQuery()
        {
            var database = new SqlConnection(Configuration.GetConnectionString("connection"));

            var query = @"select p.Id, p.Name , COUNT(*) total
                        from OrderProducts op
                            INNER join Products p on p.Id = op.ProductsId
                        GROUP by p.Id, p.Name
                        order by total DESC";

            return await database.QueryAsync<ProductSoldResponse>(query);
        }
    }
}