using Back.NET.Data;
using Back.NET.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var AllowLocalhostOrigins = "AllowLocalhostOrigins";

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);


builder.Services.AddDbContext<UserContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();



var proveedor = builder.Services.BuildServiceProvider();
var configuration = proveedor.GetService<IConfiguration>();

builder.Services.AddCors(opciones =>
{
    var frontendURL = configuration.GetValue<string>("fronted_url");

    opciones.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
