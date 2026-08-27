using MediatR;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public record ReturnBookLoanCommand(int Id) : IRequest<Unit>;
}
