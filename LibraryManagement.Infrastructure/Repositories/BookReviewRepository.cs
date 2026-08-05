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
    }
}