using FluentValidation;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class CreateBookLoanCommandHandlerValidator : AbstractValidator<CreateBookLoanCommand>
    {
        public CreateBookLoanCommandHandlerValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0.");

            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .WithMessage("Book ID must be greater than 0.");
        }
    }
}
