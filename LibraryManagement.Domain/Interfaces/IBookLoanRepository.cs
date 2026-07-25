using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookLoanRepository : IRepository<BookLoan>
    {
        Task<BookLoan> GetActiveLoanAsync(int userId, int bookId);
    }
}