using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.CustomExceptions;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Queries.Books
{
    internal class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookDetailsDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper; 
        private readonly TimeProvider _timeProvider;

        public GetBookDetailsQueryHandler(ApplicationDbContext context, IMapper mapper, TimeProvider timeProvider)
        {
            _context = context;
            _mapper = mapper;
            _timeProvider = timeProvider;
        }

        public async Task<BookDetailsDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
        {
            var utcNow = _timeProvider.GetUtcNow().UtcDateTime;

            var book = await _context.Books
                .Where(b => b.Id == request.Id)
                .ProjectTo<BookDetailsDto>(
                    _mapper.ConfigurationProvider,
                    new { userId = request.UserId, utcNow }
                )
                .FirstOrDefaultAsync(cancellationToken);

            if (book == null)
                throw new EntityNotFoundException("Book not found");

            return book;
        }
    }
}
