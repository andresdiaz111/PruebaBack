using PruebaBackAPI.Data;
using PruebaBackAPI.Models;
using PruebaBackAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Npgsql;


var builder = WebApplication.CreateBuilder(args);

var dbbuilder = new NpgsqlConnectionStringBuilder();
dbbuilder.ConnectionString = 
    builder.Configuration.GetConnectionString("PostgreSqlConnection");
    dbbuilder.Username = builder.Configuration["UserID"];
    dbbuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<UserContext>(opt => 
{
    opt.UseNpgsql(dbbuilder.ConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Swagger Prueba Back"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<IUserRepo<User>, UserRepo<User>>();
builder.Services.AddScoped<Authorizer>();
builder.Services.AddHostedService<GetUserService<User>>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
