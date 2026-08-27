using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class ReturnBookLoanCommandHandler : IRequestHandler<ReturnBookLoanCommand, Unit>
    {
        private readonly IBookLoanRepository _bookLoanRepository;

        public ReturnBookLoanCommandHandler(IBookLoanRepository bookLoanRepository)
        {
            _bookLoanRepository = bookLoanRepository;
        }

        public async Task<Unit> Handle(ReturnBookLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _bookLoanRepository.GetByIdAsync(request.Id, cancellationToken);

            if (loan == null)
                throw new EntityNotFoundException("Book loan");

            loan.MarkAsReturned();
            await _bookLoanRepository.UpdateAsync(loan, cancellationToken);

            return Unit.Value;
        }
    }
}
