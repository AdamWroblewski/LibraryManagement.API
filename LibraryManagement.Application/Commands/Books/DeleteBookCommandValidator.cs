using FluentValidation;

namespace LibraryManagement.Application.Commands.Books
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Valid Book Id is required.");
        }
    }
}
