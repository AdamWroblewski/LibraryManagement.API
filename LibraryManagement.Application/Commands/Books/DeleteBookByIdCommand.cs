using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public record DeleteBookByIdCommand(int Id) : IRequest<bool>;
}
