using FluentValidation;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class CheckoutBookLoanCommandValidator : AbstractValidator<CheckoutBookLoanCommand>
    {
        public CheckoutBookLoanCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Book loan ID must be greater than 0.");
        }
    }
}
