using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;
using LibraryManagement.Application.Queries.BookLoans;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Queries.BookLoans
{
    public class GetBookLoansQueryHandler : IRequestHandler<GetBookLoansQuery, PagedList<BookLoanDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookLoansQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<BookLoanDto>> Handle(GetBookLoansQuery request, CancellationToken cancellationToken)
        {
            var bookLoans = await _context.BookLoans
                .AsNoTracking()
                .Where(l => l.UserId == request.UserId)
                .OrderByDescending(l => l.ReservedAt)
                .ProjectTo<BookLoanDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return bookLoans;
        }
    }
}
