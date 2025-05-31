using FluentResults;
using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            string tokenResult = default;
            try
            {
                tokenResult = await _identityService.LoginAsync(request.Email, request.Password);
            }
            catch (InvalidOperationException ex)
            {
                return Result.Fail<string>(ex.Message);
            }

            return Result.Ok(tokenResult);
        }
    }
}
