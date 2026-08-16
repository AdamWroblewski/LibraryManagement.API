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
            var isBookAvailable = await _bookLoanRepository
                .IsBookAvailableAsync(request.BookId);

            if (!isBookAvailable)
                throw new InvalidOperationException("This book is not available now.");

            var bookLoan = new BookLoan(request.BookId, request.UserId);

            await _bookLoanRepository.AddAsync(bookLoan, cancellationToken);

            return bookLoan.Id;
        }
    }
}