using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class CheckoutBookLoanCommandHandler : IRequestHandler<CheckoutBookLoanCommand, Unit>
    {
        private readonly IBookLoanRepository _bookLoanRepository;

        public CheckoutBookLoanCommandHandler(IBookLoanRepository bookLoanRepository)
        {
            _bookLoanRepository = bookLoanRepository;
        }

        public async Task<Unit> Handle(CheckoutBookLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _bookLoanRepository.GetByIdAsync(request.Id, cancellationToken);

            if (loan == null)
                throw new EntityNotFoundException("Loan");

            loan.Checkout();
            await _bookLoanRepository.UpdateAsync(loan, cancellationToken);

            return Unit.Value;
        }
    }
}
