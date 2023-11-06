


using IWantApp.Endpoints.Products;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .WriteTo.MSSqlServer
        (
            context.Configuration.GetConnectionString("Connection"),
            sinkOptions: new MSSqlServerSinkOptions()
            {
                AutoCreateSqlTable = true,
                TableName = "LogApi"
            }
        );
});

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

builder.Services.AddAuthorization(e =>
{
    e.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build();

    e.AddPolicy("EmployeePolicy", e => e.RequireAuthenticatedUser().RequireClaim("EmployeeCode"));

    e.AddPolicy("EmployeePolicy011", e => e.RequireAuthenticatedUser().RequireClaim("EmployeeCode", "011"));
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

builder.Services.AddScoped<QueryAllUsersWithClaimName>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

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

app.MapMethods(ProductGet.Pattern, ProductGet.HttpMethods, ProductGet.Handler);
app.MapMethods(ProductGetAll.Pattern, ProductGetAll.HttpMethods, ProductGetAll.Handler);
app.MapMethods(ProductGetShowcase.Pattern, ProductGetShowcase.HttpMethods, ProductGetShowcase.Handler);
app.MapMethods(ProductPost.Pattern, ProductPost.HttpMethods, ProductPost.Handler);
app.MapMethods(ProductPut.Pattern, ProductPut.HttpMethods, ProductPut.Handler);
app.MapMethods(ProductDelete.Pattern, ProductDelete.HttpMethods, ProductDelete.Handler);

// app.UseExceptionHandler("/error");
// app.Map("/error", (HttpContext context) =>
// {
//     var error = context.Features?.Get<IExceptionHandlerFeature>()?.Error;

//     if (error != null)
//     {
//         if (error is SqlException)
//         {
//             return Results.Problem(title: "Database out", statusCode: 500);
//         };

//         if (error is BadHttpRequestException)
//         {
//             return Results.Problem(title: "Error to convert data to other type. See all the information sent", statusCode: 500);
//         }
//     };

//     return Results.Problem("There was an error", statusCode: 500);
// });

app.Run();