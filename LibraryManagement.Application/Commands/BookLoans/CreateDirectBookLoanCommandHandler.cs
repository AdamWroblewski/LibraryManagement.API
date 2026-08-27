using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;
using System;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public class CreateDirectBookLoanCommandHandler : IRequestHandler<CreateDirectBookLoanCommand, int>
    {
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly TimeProvider _timeProvider;

        public CreateDirectBookLoanCommandHandler(IBookLoanRepository bookLoanRepository, TimeProvider timeProvider)
        {
            _bookLoanRepository = bookLoanRepository;
            _timeProvider = timeProvider;
        }

        public async Task<int> Handle(CreateDirectBookLoanCommand request, CancellationToken cancellationToken)
        {
            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            var isBookAvailable = await _bookLoanRepository
                .IsBookAvailableAsync(request.BookId, utcNow, cancellationToken);

            if (!isBookAvailable)
                throw new DuplicateResourceException($"Book is not available for loan right now.");

            var bookLoan = BookLoan.CreateDirectLoan(request.BookId, request.UserId);
            await _bookLoanRepository.AddAsync(bookLoan);

            return bookLoan.Id;
        }
    }
}
