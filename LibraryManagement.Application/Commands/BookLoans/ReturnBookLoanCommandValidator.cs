using FluentValidation;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class ReturnBookLoanCommandValidator : AbstractValidator<ReturnBookLoanCommand>
    {
        public ReturnBookLoanCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Book loan ID must be greater than 0.");
        }
    }
}
