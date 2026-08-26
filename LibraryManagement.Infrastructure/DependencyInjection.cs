using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.BackgroundServices;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookLoanRepository, BookLoanRepository>();
            services.AddScoped<IBookReviewRepository, BookReviewRepository>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddHostedService<ExpiredReservationsCleanupService>();

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped(typeof(RoleManager<IdentityRole<int>>));

            services.AddAuthentication(options =>
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
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices
                            .GetRequiredService<UserManager<ApplicationUser>>();

                        var userIdClaim = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                        var tokenStamp = context.Principal?.FindFirstValue("securityStamp");

                        if (userIdClaim == null || tokenStamp == null)
                        {
                            context.Fail("Invalid token.");
                            return;
                        }

                        var user = await userManager.FindByIdAsync(userIdClaim);
                        if (user == null || user.SecurityStamp != tokenStamp)
                        {
                            context.Fail("Token is no longer valid.");
                        }
                    }
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}