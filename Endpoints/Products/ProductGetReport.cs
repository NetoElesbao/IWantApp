


namespace IWantApp.Endpoints.Products
{
    public class ProductGetReport
    {
        public static string Pattern => "/products/report";
        public static string[] HttpMethods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        [Authorize(policy: "EmployeeReportPolicy")]
        public static async Task<IResult> Action(QueryallproductsSold query)
        {
            return Results.Ok(await query.ExecuteQuery());
        }
    }
}