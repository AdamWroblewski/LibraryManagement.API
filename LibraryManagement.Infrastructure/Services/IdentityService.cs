using LibraryManagement.Application.Interfaces;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Identity;
using LibraryManagement.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

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

        public async Task<int> RegisterUserAsync(string email,
            string password,
            string firstName,
            string lastName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Begin an explicit database transaction
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var user = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }

            cancellationToken.ThrowIfCancellationRequested();

            await _userManager.AddToRoleAsync(user, "User");

            // Commit transaction
            await transaction.CommitAsync(cancellationToken);

            return user.Id;
        }

        public async Task<string> LoginAsync(string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidOperationException("Invalid email or password.");

            cancellationToken.ThrowIfCancellationRequested();

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                throw new InvalidOperationException("Invalid email or password.");

            cancellationToken.ThrowIfCancellationRequested();
            var token = await _tokenService.GenerateTokenAsync(user, cancellationToken);

            return token;
        }
    }
}
