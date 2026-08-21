using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public class CreateBookLoanCommandHandler : IRequestHandler<CreateBookLoanCommand, int>
    {
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly TimeProvider _timeProvider;
        public CreateBookLoanCommandHandler(IBookLoanRepository bookLoansRepository, TimeProvider timeProvider)
        {
            _bookLoanRepository = bookLoansRepository;
            _timeProvider = timeProvider;
        }

        public async Task<int> Handle(CreateBookLoanCommand request, CancellationToken cancellationToken)
        {
            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            var isBookAvailable = await _bookLoanRepository
                .IsBookAvailableAsync(request.BookId, utcNow, cancellationToken);

            if (!isBookAvailable)
                throw new InvalidOperationException("This book is not available now.");

            var bookLoan = new BookLoan(request.BookId, request.UserId);

            await _bookLoanRepository.AddAsync(bookLoan, cancellationToken);

            return bookLoan.Id;
        }
    }
}