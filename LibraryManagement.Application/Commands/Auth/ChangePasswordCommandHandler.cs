using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _identityService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword, cancellationToken);

            return Unit.Value;
        }
    }
}
