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
        public async Task<bool> IsBookAvailableAsync(int bookId, CancellationToken cancellation = default)
        {
            return !await _context.BookLoans
                .AnyAsync(l => l.BookId == bookId && l.Status != LoanStatus.Returned);
        }
    }
}