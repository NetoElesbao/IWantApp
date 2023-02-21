using IWantApp.Endpoints.Categories;
using IWantApp.Infra.Data;

namespace IWantApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionString:IWantDb"]);

            //builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapMethods(CategoryPost.Pattern, CategoryPost.Methods, CategoryPost.Handler);
            app.MapMethods(CategoryGetAll.Pattern, CategoryGetAll.Methods, CategoryGetAll.Handler);
            app.MapMethods(CategoryPut.Pattern, CategoryPut.Methods, CategoryPut.Handler);
            app.MapMethods(CategoryDelete.Pattern, CategoryDelete.Methods, CategoryDelete.Handler);

            app.Run();
        }
    }
}