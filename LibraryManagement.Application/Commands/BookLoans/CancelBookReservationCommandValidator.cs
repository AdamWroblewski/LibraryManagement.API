using FluentValidation;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class CancelBookReservationCommandValidator : AbstractValidator<CancelBookReservationCommand>
    {
        public CancelBookReservationCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Book loan ID must be greater than 0.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0.");
        }
    }
}
