using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public record CreateBookLoanCommand(int BookId, int ApplicationUserId, LoanStatus Status) : IRequest<int>;
}
