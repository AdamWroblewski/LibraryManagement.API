using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class CancelBookReservationCommandHandler : IRequestHandler<CancelBookReservationCommand, Unit>
    {
        private readonly IBookLoanRepository _bookLoanRepository;

        public CancelBookReservationCommandHandler(IBookLoanRepository bookLoanRepository)
        {
            _bookLoanRepository = bookLoanRepository;
        }

        public async Task<Unit> Handle(CancelBookReservationCommand request, CancellationToken cancellationToken)
        {
            var loan = await _bookLoanRepository.GetByIdAndUserId(request.Id, request.UserId, cancellationToken);

            if (loan == null)
                throw new EntityNotFoundException("Book loan");

            loan.CancelReservation();
            await _bookLoanRepository.UpdateAsync(loan, cancellationToken);

            return Unit.Value;
        }
    }
}
