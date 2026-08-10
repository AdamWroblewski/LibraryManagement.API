using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;

namespace LibraryManagement.Application.Interfaces.QueryServices
{
    public interface IBookQueries
    {
        Task<PagedList<BookListDto>> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
