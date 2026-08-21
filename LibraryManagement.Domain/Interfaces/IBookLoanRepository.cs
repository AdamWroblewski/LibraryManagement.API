using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookLoanRepository : IRepository<BookLoan>
    {
        Task<bool> IsBookAvailableAsync(int bookId, DateTime utcNow, CancellationToken cancellation = default);
    }
}