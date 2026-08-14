using AutoMapper;
using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Queries.Books
{
    internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDetailsDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDetailsDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Include(b => b.Loans)
                .Include(b => b.Reviews)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (book == null)
                throw new EntityNotFoundException("Book");

            return _mapper.Map<BookDetailsDto>(book);
        }
    }
}
