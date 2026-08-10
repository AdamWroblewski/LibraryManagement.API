using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces.QueryServices;
using LibraryManagement.Application.Models;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PagedList<BookListDto>>
    {
        private readonly IBookQueries _bookQueries;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IBookQueries bookQueries, IMapper mapper)
        {
            _bookQueries = bookQueries;
            _mapper = mapper;
        }

        public async Task<PagedList<BookListDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookQueries.GetPagedBooksAsync(request.PageNumber, request.PageSize, cancellationToken);
            return books;
        }
    }
}