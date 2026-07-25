using FluentResults;
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
                    new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", ISBN = "9780201616224", PublicationYear = 1999, Publisher = "Addison-Wesley" },
                    new Book { Title = "Clean Code", Author = "Robert C. Martin", ISBN = "9780132350884", PublicationYear = 2008, Publisher = "Prentice Hall" },
                    new Book { Title = "Design Patterns", Author = "Erich Gamma", ISBN = "9780201633610", PublicationYear = 1994, Publisher = "Addison-Wesley" },
                    new Book { Title = "Refactoring", Author = "Martin Fowler", ISBN = "9780201485677", PublicationYear = 1999, Publisher = "Addison-Wesley" },
                    new Book { Title = "Domain-Driven Design", Author = "Eric Evans", ISBN = "9780321125217", PublicationYear = 2003, Publisher = "Addison-Wesley" },
                    new Book { Title = "Effective C#", Author = "Bill Wagner", ISBN = "9780321245663", PublicationYear = 2010, Publisher = "Addison-Wesley" },
                    new Book { Title = "C# in Depth", Author = "Jon Skeet", ISBN = "9781617294532", PublicationYear = 2019, Publisher = "Manning" },
                    new Book { Title = "Pro ASP.NET Core", Author = "Adam Freeman", ISBN = "9781484254394", PublicationYear = 2020, Publisher = "Apress" },
                    new Book { Title = "Head First Design Patterns", Author = "Eric Freeman", ISBN = "9780596007126", PublicationYear = 2004, Publisher = "O'Reilly" },
                    new Book { Title = "Patterns of Enterprise Application Architecture", Author = "Martin Fowler", ISBN = "9780321127426", PublicationYear = 2002, Publisher = "Addison-Wesley" },
                    new Book { Title = "Test-Driven Development", Author = "Kent Beck", ISBN = "9780321146533", PublicationYear = 2002, Publisher = "Addison-Wesley" },
                    new Book { Title = "Working Effectively with Legacy Code", Author = "Michael Feathers", ISBN = "9780131177055", PublicationYear = 2004, Publisher = "Prentice Hall" },
                    new Book { Title = "You Don't Know JS", Author = "Kyle Simpson", ISBN = "9781491904244", PublicationYear = 2015, Publisher = "O'Reilly" },
                    new Book { Title = "JavaScript: The Good Parts", Author = "Douglas Crockford", ISBN = "9780596517748", PublicationYear = 2008, Publisher = "O'Reilly" },
                    new Book { Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", ISBN = "9780262033848", PublicationYear = 2009, Publisher = "MIT Press" },
                    new Book { Title = "Cracking the Coding Interview", Author = "Gayle Laakmann McDowell", ISBN = "9780984782857", PublicationYear = 2015, Publisher = "CareerCup" },
                    new Book { Title = "The Art of Computer Programming", Author = "Donald Knuth", ISBN = "9780201896831", PublicationYear = 1997, Publisher = "Addison-Wesley" },
                    new Book { Title = "Code Complete", Author = "Steve McConnell", ISBN = "9780735619678", PublicationYear = 2004, Publisher = "Microsoft Press" },
                    new Book { Title = "The Mythical Man-Month", Author = "Frederick P. Brooks Jr.", ISBN = "9780201835953", PublicationYear = 1995, Publisher = "Addison-Wesley" },
                    new Book { Title = "Structure and Interpretation of Computer Programs", Author = "Harold Abelson", ISBN = "9780262510875", PublicationYear = 1996, Publisher = "MIT Press" },
                };

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
