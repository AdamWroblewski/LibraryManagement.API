using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetPagedBooksQuery(int PageNumber, int PageSize) : IRequest<PagedList<BookDto>>;
}