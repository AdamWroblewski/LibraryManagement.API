using LibraryManagement.API.DataSeed;
using LibraryManagement.API.Extensions;
using LibraryManagement.Application;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(
    Environment.GetEnvironmentVariable("HOME") ?? ".",
    "LogFiles",
    "Application",
    "errors-.txt");

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(options =>
    options.CustomizeProblemDetails = ctx =>
    {
        //ctx.ProblemDetails.Extensions.Remove("exception");
        //ctx.ProblemDetails.Extensions.Remove("headers");
    }
);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
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

// Add services
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Logs to console
builder.Logging.AddDebug();   // Logs to debug window

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors("AllowSpecificOrigins");

// For now allow swagger at production
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnablePersistAuthorization();
});

app.MapGet("/", () => Results.Redirect("/swagger"));

if (app.Environment.IsDevelopment())
{
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