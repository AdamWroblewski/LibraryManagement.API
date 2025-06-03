using FluentValidation;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Commands.Books
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(IBookRepository bookRepository)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required.");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.");
            RuleFor(x => x.Publisher).NotEmpty().WithMessage("Publisher is required.");
            RuleFor(x => x.PublicationYear).NotEmpty().WithMessage("PublicationYear is required.");
        }
    }
}
