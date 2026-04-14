using DistributionWaterApp.Database;
using DistributionWaterApp.Services.Barrios;
using DistributionWaterApp.Services.TurnosAgua;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBarrioService, BarrioService>();
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddScoped<ITurnoAguaService, TurnoAguaService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
