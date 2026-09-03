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

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var adminEmail = "admin@library.demo";
            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Employee", "User" });
                }
            }

            var employeeEmail = "employee@library.demo";
            if (await userManager.FindByEmailAsync(employeeEmail) is null)
            {
                var employeeUser = new ApplicationUser
                {
                    UserName = "employee",
                    Email = employeeEmail,
                    FirstName = "Library",
                    LastName = "Employee"
                };

                var result = await userManager.CreateAsync(employeeUser, "Employee123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(employeeUser, new[] { "Employee", "User" });
                }
            }

            var userEmail = "user@library.demo";
            if (await userManager.FindByEmailAsync(userEmail) is null)
            {
                var regularUser = new ApplicationUser
                {
                    UserName = "testuser",
                    Email = userEmail,
                    FirstName = "John",
                    LastName = "Doe"
                };

                var result = await userManager.CreateAsync(regularUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }
        }

        public static void SeedBooksAndRelatedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
                    new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", "9780321127426", 2002, "Addison-Wesley")
                };

                context.Books.AddRange(books);
                context.SaveChanges();
            }

            var testUser = userManager.FindByEmailAsync("user@library.demo").GetAwaiter().GetResult();
            var adminUser = userManager.FindByEmailAsync("admin@library.demo").GetAwaiter().GetResult();

            if (testUser != null && adminUser != null)
            {
                SeedLoans(context, testUser.Id, adminUser.Id);
                SeedReviews(context, testUser.Id, adminUser.Id);
            }
        }

        private static void SeedLoans(ApplicationDbContext context, int userId, int adminId)
        {
            if (!context.BookLoans.Any())
            {
                var books = context.Books.Take(8).ToList();
                if (books.Count < 8) return;

                // 1. Reserved Loans
                var reserved1 = new BookLoan(books[0].Id, userId);
                var reserved2 = new BookLoan(books[1].Id, adminId);

                // 2. Direct Active Loans
                var active1 = BookLoan.CreateDirectLoan(books[2].Id, userId);
                var active2 = BookLoan.CreateDirectLoan(books[3].Id, adminId);

                // 3. Overdue Loans (Transitioned from Direct Loan)
                var overdue1 = BookLoan.CreateDirectLoan(books[4].Id, userId);
                SetPrivateProperty(overdue1, nameof(BookLoan.Status), LoanStatus.Overdue);
                SetPrivateProperty(overdue1, nameof(BookLoan.CheckedOutAt), DateTime.UtcNow.AddDays(-35));
                SetPrivateProperty(overdue1, nameof(BookLoan.DueAt), DateTime.UtcNow.AddDays(-7));

                // 4. Returned Loans
                var returned1 = BookLoan.CreateDirectLoan(books[5].Id, userId);
                returned1.MarkAsReturned();
                SetPrivateProperty(returned1, nameof(BookLoan.CheckedOutAt), DateTime.UtcNow.AddDays(-60));
                SetPrivateProperty(returned1, nameof(BookLoan.DueAt), DateTime.UtcNow.AddDays(-32));
                SetPrivateProperty(returned1, nameof(BookLoan.ReturnedAt), DateTime.UtcNow.AddDays(-30));

                // 5. Expired Reservation
                var expired1 = new BookLoan(books[6].Id, userId);
                expired1.MarkAsExpired();
                SetPrivateProperty(expired1, nameof(BookLoan.ReservedAt), DateTime.UtcNow.AddHours(-80));

                // 6. Cancelled Reservation
                var cancelled1 = new BookLoan(books[7].Id, adminId);
                cancelled1.CancelReservation();

                context.BookLoans.AddRange(reserved1, reserved2, active1, active2, overdue1, returned1, expired1, cancelled1);
                context.SaveChanges();
            }
        }

        private static void SeedReviews(ApplicationDbContext context, int userId, int adminId)
        {
            if (!context.BookReviews.Any())
            {
                var books = context.Books.Take(6).ToList();
                if (books.Count < 6) return;

                var reviews = new List<BookReview>
                {
                    new BookReview(books[0].Id, userId, 5, "A timeless classic! Every developer should read this at least once."),
                    new BookReview(books[0].Id, adminId, 5, "Incredible insights into software craftsmanship and mindset."),
                    new BookReview(books[1].Id, userId, 4, "Solid principles for writing clean, maintainable code. Highly recommended."),
                    new BookReview(books[2].Id, adminId, 5, "Essential reference book for object-oriented software design patterns."),
                    new BookReview(books[3].Id, userId, 4, "Great techniques for refactoring legacy code safely."),
                    new BookReview(books[4].Id, adminId, 5, "Game changer for understanding domain complexity and model architecture."),
                    new BookReview(books[5].Id, userId, 3, "Good tips for modern C#, though some sections feel a bit dated.")
                };

                context.BookReviews.AddRange(reviews);
                context.SaveChanges();
            }
        }

        private static void SetPrivateProperty<T>(T obj, string propertyName, object val)
        {
            var prop = typeof(T).GetProperty(propertyName);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, val, null);
            }
            else
            {
                var field = typeof(T).GetField($"<{propertyName}>k__BackingField",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                field?.SetValue(obj, val);
            }
        }
    }
}
