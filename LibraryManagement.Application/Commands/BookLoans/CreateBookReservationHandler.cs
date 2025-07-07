using System.Security.Claims;
using AutoMapper;
using FluentResults;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public class CreateBookReservationHandler : IRequestHandler<CreateBookReservationCommand, Result>
    {
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly IMapper _mapper;
        public CreateBookReservationHandler(IBookLoanRepository bookLoansRepository, IMapper mapper)
        {
            _bookLoanRepository = bookLoansRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateBookReservationCommand request, CancellationToken cancellationToken)
        {
            var existingReservations = await _bookLoanRepository
                .GetActiveLoanAsync(request.applicationUserId, request.bookId);

            if (existingReservations != null)
            {
                return Result.Fail("You already have an active reservation for this book.");
            }

            BookLoan bookLoan = _mapper.Map<BookLoan>(request);
            bookLoan.IsReservation = true;
            bookLoan.ReservationDate = DateTime.UtcNow;

            await _bookLoanRepository.AddAsync(bookLoan);

            return Result.Ok();
        }
    }
}