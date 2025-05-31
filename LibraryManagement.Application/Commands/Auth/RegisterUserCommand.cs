using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class RegisterUserCommand : IRequest<Result<Guid>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}