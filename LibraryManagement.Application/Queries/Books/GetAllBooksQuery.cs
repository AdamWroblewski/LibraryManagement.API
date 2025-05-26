using LibraryManagement.Application.DTOs;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetAllBooksQuery : IRequest<List<BookDto>>
    {
    }
}