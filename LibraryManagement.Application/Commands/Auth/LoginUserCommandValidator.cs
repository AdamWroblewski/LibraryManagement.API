using FluentValidation;

namespace LibraryManagement.Application.Commands.Auth
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(254).WithMessage("Email address cannot exceed 254 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(128).WithMessage("Password cannot exceed 128 characters.");
        }
    }
}
