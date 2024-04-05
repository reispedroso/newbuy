using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using newbuy.App.Services;
using newbuy.Domain.Interfaces;
using newbuy.Infrastructure.Data;
using newbuy.Infrastructure.Repositories;
using newbuy.Presentations.Controllers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PasswordHash>();
builder.Services.AddSingleton<DateTimeCorrection>();

builder.Services.AddScoped<IUserInterface, UserRepository>();
builder.Services.AddScoped<IProductInterface, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<ProductController>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var key = Encoding.ASCII.GetBytes(newbuy.App.Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();

