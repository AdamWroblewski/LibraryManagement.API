using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.API.DataSeed
{
    public static class SeedRoles
    {
        public static async Task Seed(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Admin", "User", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
    }
}
