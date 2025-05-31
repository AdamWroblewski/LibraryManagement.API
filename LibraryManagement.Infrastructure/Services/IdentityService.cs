using FluentResults;
using LibraryManagement.Application.Interfaces;
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

        public IdentityService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Guid> RegisterUserAsync(string email, string password, string firstName, string lastName)
        {
            var user = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }
            return user.Id;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidOperationException("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                throw new InvalidOperationException("Invalid email or password.");

            var token = await _tokenService.GenerateTokenAsync(user);
            return token;
        }
    }
}
