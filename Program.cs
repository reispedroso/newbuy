using Microsoft.EntityFrameworkCore;
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



var app = builder.Build();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

