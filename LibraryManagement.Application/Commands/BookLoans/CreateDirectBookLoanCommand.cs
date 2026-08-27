using MediatR;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public record CreateDirectBookLoanCommand(int BookId, int UserId) : IRequest<int>;
}
