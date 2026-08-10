using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookLoanRepository : IRepository<BookLoan>
    {
        Task<BookLoan?> GetReservedOrActiveLoanAsync(int userId, int bookId, CancellationToken cancellation = default);
    }
}