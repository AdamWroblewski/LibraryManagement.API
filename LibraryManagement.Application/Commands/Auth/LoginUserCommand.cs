using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class LoginUserCommand : IRequest<Result<string>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
