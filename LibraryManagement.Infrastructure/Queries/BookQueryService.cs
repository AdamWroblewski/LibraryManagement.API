using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces.QueryServices;
using LibraryManagement.Application.Models;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Queries
{
    public class BookQueryService : IBookQueryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookQueryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<BookListDto>> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ProjectTo<BookListDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}
