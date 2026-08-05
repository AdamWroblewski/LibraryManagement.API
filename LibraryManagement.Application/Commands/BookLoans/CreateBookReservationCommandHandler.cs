using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public class CreateBookReservationCommandHandler : IRequestHandler<CreateBookReservationCommand, int>
    {
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly IMapper _mapper;
        public CreateBookReservationCommandHandler(IBookLoanRepository bookLoansRepository, IMapper mapper)
        {
            _bookLoanRepository = bookLoansRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBookReservationCommand request, CancellationToken cancellationToken)
        {
            var existingReservation = await _bookLoanRepository
                .GetActiveLoanAsync(request.applicationUserId, request.bookId);

            if (existingReservation != null)
            {
                throw new InvalidOperationException("You already have an active reservation for this book.");
            }

            var bookLoan = new BookLoan(request.bookId, request.applicationUserId, true, DateTime.UtcNow);

            await _bookLoanRepository.AddAsync(bookLoan);

            return bookLoan.Id;
        }
    }
}