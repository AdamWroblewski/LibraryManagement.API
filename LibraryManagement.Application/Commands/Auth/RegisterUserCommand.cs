using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : IRequest<int>;
}