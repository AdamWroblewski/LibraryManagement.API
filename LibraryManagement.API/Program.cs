using FluentValidation;
using LibraryManagement.API.DataSeed;
using LibraryManagement.API.Extensions;
using LibraryManagement.Application;
using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(
    Environment.GetEnvironmentVariable("HOME") ?? ".",
    "LogFiles",
    "Application",
    "errors-.txt");


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
    {
        policy.WithOrigins(
                  "http://localhost:4200",
                  "https://thankful-cliff-089e04a03.2.azurestaticapps.net"
              )
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

// Add services
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Logs to console
builder.Logging.AddDebug();   // Logs to debug window

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature == null) return;

        var exception = exceptionHandlerFeature.Error;
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                logger.LogWarning(exception, "Validation failed for request: {Path}", context.Request.Path);

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                await context.Response.WriteAsJsonAsync(new { errors });
                break;

            case EntityNotFoundException:
            case KeyNotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                logger.LogWarning(exception, "Resource not found at path: {Path}", context.Request.Path);
                await context.Response.WriteAsJsonAsync(new { errors = exception.Message });
                break;

            case InvalidOperationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                logger.LogWarning(exception, "Invalid operation attempt: {Path}", context.Request.Path);
                await context.Response.WriteAsJsonAsync(new { errors = exception.Message });
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                logger.LogError(exception, "An unhandled exception occurred while processing path: {Path}", context.Request.Path);
                await context.Response.WriteAsJsonAsync(new { errors = "An unhandled server error occurred." });
                break;
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations<ApplicationDbContext>();
    await SeedDataAsync(app);
}

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Console.WriteLine("Seeding data...");
    await SeedDataAsync(app);
    Console.WriteLine("Seeding complete. Exiting.");
    return;
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

async Task SeedDataAsync(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

            await SeedData.SeedRoles(roleManager);
            await SeedData.SeedAdmin(userManager, roleManager);
            SeedData.SeedBooks(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during data seeding.");
        }
    }
}