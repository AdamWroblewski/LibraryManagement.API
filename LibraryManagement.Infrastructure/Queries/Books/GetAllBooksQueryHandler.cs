using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetPagedBooksQuery, PagedList<BookListDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<PagedList<BookListDto>> Handle(GetPagedBooksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ProjectTo<BookListDto>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}