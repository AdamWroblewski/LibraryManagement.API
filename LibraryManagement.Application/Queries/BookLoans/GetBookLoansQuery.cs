using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Queries.BookLoans
{
    public record GetBookLoansQuery([property: JsonIgnore] int UserId,
        int PageNumber,
        int PageSize) : IRequest<PagedList<BookLoanDto>>;
}
