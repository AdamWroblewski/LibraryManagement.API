using FluentValidation;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class DeleteBookReviewCommandValidator : AbstractValidator<DeleteBookReviewCommand>
    {
        public DeleteBookReviewCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .WithMessage("Book ID must be greater than 0.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0.");
        }
    }
}