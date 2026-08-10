using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Books
                .Include(b => b.Loans)
                .Include(b => b.Reviews)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }
    }
}