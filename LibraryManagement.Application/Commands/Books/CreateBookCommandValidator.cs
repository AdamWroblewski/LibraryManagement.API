using FluentValidation;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required.");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.");
            RuleFor(x => x.Publisher).NotEmpty().WithMessage("Publisher is required.");
            RuleFor(x => x.PublicationYear).NotEmpty().WithMessage("PublicationYear is required.");
        }
    }
}
