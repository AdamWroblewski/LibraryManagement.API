using FluentResults;

namespace LibraryManagement.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Guid> RegisterUserAsync(string email, string password, string firstName, string lastName);
        Task<string> LoginAsync(string email, string password);
    }
}
