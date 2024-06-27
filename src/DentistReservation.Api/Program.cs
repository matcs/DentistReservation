using DentistReservation.Application;
using DentistReservation.Infrastructure;
using DentistReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();

builder.Services.AddInfrastructure();
builder.Services.AddServices();

var app = builder.Build();

using var scope = app.Services.CreateScope();
ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if (context.Database.GetPendingMigrations().Any())
     context.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();