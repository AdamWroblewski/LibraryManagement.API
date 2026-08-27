using MediatR;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public record CheckoutBookLoanCommand(int Id) : IRequest<Unit>
    {
    }
}
