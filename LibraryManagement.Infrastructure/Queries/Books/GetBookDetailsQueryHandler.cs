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

        public GetBookDetailsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDetailsDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider,
                    new { userId = request.UserId })
                .Where(b => b.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (book == null)
                throw new EntityNotFoundException("Book not found");

            return book;
        }
    }
}
