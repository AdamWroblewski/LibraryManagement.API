using LibraryManagement.Application.DTOs;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetBookByIdQuery(int Id) : IRequest<BookDto>;
}
