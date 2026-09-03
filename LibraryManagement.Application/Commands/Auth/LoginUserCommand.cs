using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public record LoginUserCommand(string Email, string Password) : IRequest<string>;
}
