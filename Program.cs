using Microsoft.EntityFrameworkCore;
using newbuy.App.Services;
using newbuy.Domain.Interfaces;
using newbuy.Infrastructure.Data;
using newbuy.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PasswordHash>();
builder.Services.AddSingleton<DateTimeCorrection>();

builder.Services.AddScoped<UserInterface, UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.Run();

