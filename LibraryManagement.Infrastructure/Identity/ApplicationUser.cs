using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Infrastructure.Identity
{
    public class  ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    }
}