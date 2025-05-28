using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.DTOs
{
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; private set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
