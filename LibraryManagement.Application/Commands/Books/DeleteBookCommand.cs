using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public record DeleteBookCommand(int Id) : IRequest<Unit>;
}
