namespace LibraryManagement.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<int> RegisterUserAsync(string email,
            string password,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default);
        
        Task<int> CreateEmployeeAsync(string email,
            string password,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default);

        Task<string> LoginAsync(string email,
            string password,
            CancellationToken cancellationToken = default);

        Task ChangePasswordAsync(int userId, 
            string currentPassword, 
            string newPassword, 
            CancellationToken cancellationToken = default);
    }
}
