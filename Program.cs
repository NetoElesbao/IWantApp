using IWantApp.Endpoints.Categories;
using IWantApp.Endpoints.Employees;
using IWantApp.Endpoints.Security;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    // aqui se pode personalizar a convensão das senhas
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<QueryAllUsersWithClaimName>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryGetAll.Pattern, CategoryGetAll.HttpMethods, CategoryGetAll.Handler);
app.MapMethods(CategoryPost.Pattern, CategoryPost.HttpMethods, CategoryPost.Handler);
app.MapMethods(CategoryPut.Pattern, CategoryPut.HttpMethods, CategoryPut.Handler);
app.MapMethods(CategoryDelete.Pattern, CategoryDelete.HttpMethods, CategoryDelete.Handler);

app.MapMethods(EmployeesPost.Pattern, EmployeesPost.HttpMethods, EmployeesPost.Handler);
app.MapMethods(EmployeesGetAll.Pattern, EmployeesGetAll.HttpMethods, EmployeesGetAll.Handler);

app.MapMethods(TokenPost.Pattern, TokenPost.HttpMethods, TokenPost.Handler);

app.Run();