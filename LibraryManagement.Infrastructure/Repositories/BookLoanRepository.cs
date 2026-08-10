using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookLoanRepository : BaseRepository<BookLoan>, IBookLoanRepository
    {
        public BookLoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves all active book reservations, reservation is active for 5 days from the reservation date, 
        /// the reservation day is counted as one whole day
        /// </summary>
        /// <returns>A list of active <see cref="BookLoan"/> entities.</returns>
        public async Task<BookLoan?> GetReservedOrActiveLoanAsync(int userId, int bookId, CancellationToken cancellation = default)
        {
            return await _context.BookLoans
                .Where(l => l.UserId == userId &&
                    l.BookId == bookId &&
                    (l.Status == LoanStatus.Reserved || l.Status == LoanStatus.Active))
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellation);
        }
    }
}