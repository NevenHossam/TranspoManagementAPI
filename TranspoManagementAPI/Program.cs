
using TranspoManagementAPI.Mapping;
using Microsoft.EntityFrameworkCore;
using TranspoManagementAPI.Data;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Repositories;
using TranspoManagementAPI.Services;
using TranspoManagementAPI.IServices;
using TranspoManagementAPI.Repositories.Implementations;
using TranspoManagementAPI.Services.Implementations;
using TranspoManagementAPI.Services.Interfaces;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TranspoManagementAPI.Validators.FareBandRequestDtoValidator>();

// Configure SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=transpo.db"));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Register services
builder.Services.AddScoped<IFareBandRepository, FareBandRepository>();
builder.Services.AddScoped<IFareBandService, FareBandService>();
builder.Services.AddScoped<IFareCalcService, FareCalcService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Transpo Management API",
        Version = "v1",
        Description = "API for managing transportation fares, vehicles, and trips.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com"
        }
    });


});

builder.Services.AddControllers();
var app = builder.Build();
// Use global exception handling middleware
app.UseMiddleware<TranspoManagementAPI.Middleware.ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // creates and seeds DB on first run
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
