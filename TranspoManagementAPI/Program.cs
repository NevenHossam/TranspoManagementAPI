using TranspoManagementAPI.Mapping;
using Microsoft.EntityFrameworkCore;
using TranspoManagementAPI.Data;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Repositories;
using TranspoManagementAPI.Services;
using TranspoManagementAPI.Repositories.Implementations;
using TranspoManagementAPI.Services.Implementations;
using TranspoManagementAPI.Services.Interfaces;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configure the port based on environment variable (for Render deployment)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5090";
builder.WebHost.UseUrls($"http://*:{port}");

// Register in-memory cache
builder.Services.AddMemoryCache();

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
    });
});

// Rate Limiter with header-based partitioning for test clients
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        var clientId = httpContext.Request.Headers["X-Test-Client"].ToString();
        if (!string.IsNullOrEmpty(clientId))
        {
            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: clientId,
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromSeconds(60),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0
                });
        }

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromSeconds(60),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });

    options.RejectionStatusCode = 429;
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseRateLimiter();
app.UseMiddleware<TranspoManagementAPI.Middleware.ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

// Add health check endpoint for Render
app.MapGet("/health", () => "Healthy!");

app.Run();

// for WebApplicationFactory in Rate limiter cases
public partial class Program { }
