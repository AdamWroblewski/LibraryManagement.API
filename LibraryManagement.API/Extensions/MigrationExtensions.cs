using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations<T>(this IApplicationBuilder app) where T : DbContext
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using T dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
        }
    }
}
