using LibraryManagement.Infrastructure.Identity;

namespace LibraryManagement.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
