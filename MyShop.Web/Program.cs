using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyShop.Application.Feature.User.Validators;
using MyShop.Data.Context;
using MyShop.Domain.Common;
using MyShop.IOC.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("MyShopApiConnectionStrings")?? "";

builder.Services.AddDbContext<MyShopContext>(option =>
{
    option.UseSqlServer(connectionString);
});

builder.Services.IOC();

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MyApiSecurityKey>(builder.Configuration.GetSection("MyApiSecurityKey"));


#region Jwt

string Signiture = builder.Configuration.GetValue<string>("MyApiSecurityKey:SecurityKey")?? "";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidIssuer = "https://localhost:7151",
            ValidAudience = "https://localhost:7151",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Signiture)),
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });

#endregion
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();