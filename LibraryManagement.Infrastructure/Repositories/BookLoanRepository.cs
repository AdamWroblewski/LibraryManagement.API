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
        /// </summary>
        /// <returns>A list of active <see cref="BookLoan"/> entities.</returns>
        public async Task<bool> IsBookAvailableAsync(int bookId, DateTime utcNow, CancellationToken cancellationToken = default)
        {
            return !await _context.BookLoans
                .AnyAsync(l => l.BookId == bookId && (
                    l.Status == LoanStatus.Active ||
                    l.Status == LoanStatus.Overdue ||
                    (l.Status == LoanStatus.Reserved && l.ReservedAt.AddHours(BookLoan.ReservationHoldPolicyHours) > utcNow)
                ), cancellationToken);
        }

        public async Task<BookLoan?> GetByIdAndUserId(int id, int userId, CancellationToken cancellationToken = default)
        {
            return await _context.BookLoans.SingleOrDefaultAsync(l => l.Id == id && l.UserId == userId, cancellationToken);
        }
    }
}