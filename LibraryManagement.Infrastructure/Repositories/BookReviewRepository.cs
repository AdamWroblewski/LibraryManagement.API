using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookReviewRepository : BaseRepository<BookReview>, IBookReviewRepository
    {
        public BookReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> HasReviewAsync(int bookId, int userId, CancellationToken cancellationToken = default)
        {
            return await _context.BookReviews.AnyAsync(b => b.BookId == bookId && b.UserId == userId, cancellationToken);
        }

        public async Task<BookReview?> GetByBookIdAndUserId(int bookId, int userId, CancellationToken cancellationToken = default)
        {
            return await _context.BookReviews.SingleOrDefaultAsync(b => b.BookId == bookId && b.UserId == userId, cancellationToken);
        }
    }
}