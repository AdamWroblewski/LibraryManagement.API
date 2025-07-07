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
        public async Task<BookLoan> GetActiveLoanAsync(int userId, int bookId)
        {
            var fiveDaysAgo = DateTime.UtcNow.Date.AddDays(-5);

            return await _context.BookLoans
                .Where(l => l.ApplicationUserId == userId && l.BookId == bookId &&
                    (
                        (l.LoanDate != null &&
                        l.ReturnDate == null) ||
                        (l.LoanDate == null &&
                        l.IsReservation && 
                        l.ReservationDate != null && 
                        l.ReservationDate.Value.Date >= fiveDaysAgo)
                    )
                )
                .FirstOrDefaultAsync();
        }
    }
}