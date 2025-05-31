using System.Reflection;
using LibraryManagement.Application.Behaviors;
using LibraryManagement.Application.Commands.Auth;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}