using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Queries.BookLoans;
using LibraryManagement.Domain.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetActiveLoansQueryHandler : IRequestHandler<GetActiveLoansQuery, ActiveBookLoanDto>
    {
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly IMapper _mapper;

        public GetActiveLoansQueryHandler(IBookLoanRepository bookRepository, IMapper mapper)
        {
            _bookLoanRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ActiveBookLoanDto> Handle(GetActiveLoansQuery request, CancellationToken cancellationToken)
        {
            var activeBookLoan = await _bookLoanRepository.GetActiveLoanAsync(request.applicationUserId, request.bookId);
            return _mapper.Map<ActiveBookLoanDto>(activeBookLoan);
        }
    }
}