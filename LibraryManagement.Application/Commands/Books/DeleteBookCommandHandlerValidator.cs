using FluentValidation;

namespace LibraryManagement.Application.Commands.Books
{
    public class DeleteBookCommandHandlerValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandHandlerValidator()
        {
            RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Valid Book Id is required.");
        }
    }
}
