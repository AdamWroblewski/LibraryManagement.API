using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class RegisterUserCommand : IRequest<Result<Guid>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}