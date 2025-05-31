using FluentResults;
using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Guid userId;
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
                return Result.Fail<Guid>(ex.Message);
            }

            return Result.Ok(userId);
        }
    }
}
