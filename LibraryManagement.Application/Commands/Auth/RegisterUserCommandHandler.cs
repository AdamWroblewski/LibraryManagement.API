using FluentResults;
using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<int>>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            int userId;
            try
            {
                userId = await _identityService.RegisterUserAsync(
                        request.Email,
                        request.Password,
                        request.FirstName,
                        request.LastName);
            }
            catch (InvalidOperationException ex)
            {
                return Result.Fail<int>(ex.Message);
            }

            return Result.Ok(userId);
        }
    }
}
