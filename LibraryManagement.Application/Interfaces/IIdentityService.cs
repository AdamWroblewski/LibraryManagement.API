namespace LibraryManagement.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<int> RegisterUserAsync(string email,
            string password,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default);

        Task<string> LoginAsync(string email,
            string password,
            CancellationToken cancellationToken = default);
    }
}
