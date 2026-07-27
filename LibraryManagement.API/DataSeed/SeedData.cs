using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.API.DataSeed
{
    public static class SeedData
    {
        public static async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles = { "Admin", "User", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }

        public static async Task SeedAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var email = "admin@admin";
            if (await userManager.FindByEmailAsync(email) is null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = email,
                    FirstName = "admin",
                    LastName = "admin"
                };

                var result = await userManager.CreateAsync(user, "Password1!");

                // If the user was created successfully, add them to the desired roles
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, new[] { "Admin", "Employee", "User" });
                }
            }
        }

        public static void SeedBooks(ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book("The Pragmatic Programmer", "Andrew Hunt", "9780201616224", 1999, "Addison-Wesley"),
                    new Book("Clean Code", "Robert C. Martin", "9780132350884", 2008, "Prentice Hall"),
                    new Book("Design Patterns", "Erich Gamma", "9780201633610", 1994, "Addison-Wesley"),
                    new Book("Refactoring", "Martin Fowler", "9780201485677", 1999, "Addison-Wesley"),
                    new Book("Domain-Driven Design", "Eric Evans", "9780321125217", 2003, "Addison-Wesley"),
                    new Book("Effective C#", "Bill Wagner", "9780321245663", 2010, "Addison-Wesley"),
                    new Book("C# in Depth", "Jon Skeet", "9781617294532", 2019, "Manning"),
                    new Book("Pro ASP.NET Core", "Adam Freeman", "9781484254394", 2020, "Apress"),
                    new Book("Head First Design Patterns", "Eric Freeman", "9780596007126", 2004, "O'Reilly"),
                    new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", "9780321127426", 2002, "Addison-Wesley"),
                    new Book("Test-Driven Development", "Kent Beck", "9780321146533", 2002, "Addison-Wesley"),
                    new Book("Working Effectively with Legacy Code", "Michael Feathers", "9780131177055", 2004, "Prentice Hall"),
                    new Book("You Don't Know JS", "Kyle Simpson", "9781491904244", 2015, "O'Reilly"),
                    new Book("JavaScript: The Good Parts", "Douglas Crockford", "9780596517748", 2008, "O'Reilly"),
                    new Book("Introduction to Algorithms", "Thomas H. Cormen", "9780262033848", 2009, "MIT Press"),
                    new Book("Cracking the Coding Interview", "Gayle Laakmann McDowell", "9780984782857", 2015, "CareerCup"),
                    new Book("The Art of Computer Programming", "Donald Knuth", "9780201896831", 1997, "Addison-Wesley"),
                    new Book("Code Complete", "Steve McConnell", "9780735619678", 2004, "Microsoft Press"),
                    new Book("The Mythical Man-Month", "Frederick P. Brooks Jr.", "9780201835953", 1995, "Addison-Wesley"),
                    new Book("Structure and Interpretation of Computer Programs", "Harold Abelson", "9780262510875", 1996, "MIT Press")
                };

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
