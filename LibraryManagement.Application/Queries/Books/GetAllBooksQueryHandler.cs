using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces.QueryServices;
using LibraryManagement.Application.Models;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PagedList<BookListDto>>
    {
        private readonly IBookQueryService _bookQueryService;

        public GetAllBooksQueryHandler(IBookQueryService bookQueries)
        {
            _bookQueryService = bookQueries;
        }

        public async Task<PagedList<BookListDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookQueryService.GetPagedBooksAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}