using FluentValidation;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class UpdateBookReviewCommandValidator : AbstractValidator<UpdateBookReviewCommand>
    {
        public UpdateBookReviewCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .WithMessage("Book ID must be greater than 0.");

            RuleFor(x => x.Rate)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(1000)
                .WithMessage("Comment cannot exceed 1000 characters.");
        }
    }
}
