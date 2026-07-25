using LibraryManagement.Application.DTOs;
using MediatR;

namespace LibraryManagement.Application.Queries.BookLoans
{
    public record GetActiveLoansQuery(int bookId, int applicationUserId) : IRequest<ActiveBookLoanDto>;
}
