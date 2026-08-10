using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public class CreateBookLoanCommandHandler : IRequestHandler<CreateBookLoanCommand, int>
    {
        private readonly IBookLoanRepository _bookLoanRepository;
        public CreateBookLoanCommandHandler(IBookLoanRepository bookLoansRepository)
        {
            _bookLoanRepository = bookLoansRepository;
        }

        public async Task<int> Handle(CreateBookLoanCommand request, CancellationToken cancellationToken)
        {
            var existingReservation = await _bookLoanRepository
                .GetReservedOrActiveLoanAsync(request.ApplicationUserId, request.BookId);

            if (existingReservation != null)
            {
                throw new InvalidOperationException("You already have an active reservation for this book.");
            }

            var bookLoan = new BookLoan(request.BookId, request.ApplicationUserId, request.Status);

            await _bookLoanRepository.AddAsync(bookLoan, cancellationToken);

            return bookLoan.Id;
        }
    }
}