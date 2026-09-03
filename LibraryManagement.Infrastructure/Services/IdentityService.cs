using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using LibraryManagement.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public IdentityService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<int> RegisterUserAsync(
            string email,
            string password,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default)
        {
            return await CreateUserWithRoleAsync(email, password, firstName, lastName, "User", cancellationToken);
        }

        public async Task<int> CreateEmployeeAsync(
            string email,
            string password,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default)
        {
            return await CreateUserWithRoleAsync(email, password, firstName, lastName, "Employee", cancellationToken);
        }

        private async Task<int> CreateUserWithRoleAsync(string email,
            string password,
            string firstName,
            string lastName,
            string roleName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();


            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);
                    throw new UserRegistrationFailedException(errors);
                }

                cancellationToken.ThrowIfCancellationRequested();

                await _userManager.AddToRoleAsync(user, roleName);

                await transaction.CommitAsync(cancellationToken);

                return user.Id;
            });
        }

        public async Task<string> LoginAsync(string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidCredentialsException();

            cancellationToken.ThrowIfCancellationRequested();

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (result.IsLockedOut)
                throw new AccountLockedOutException();

            if (!result.Succeeded)
                throw new InvalidCredentialsException();

            cancellationToken.ThrowIfCancellationRequested();
            var token = await _tokenService.GenerateTokenAsync(user, cancellationToken);

            return token;
        }

        public async Task ChangePasswordAsync(int userId,
            string currentPassword,
            string newPassword,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new EntityNotFoundException("User");

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
                throw new InvalidCredentialsException();
        }
    }
}
