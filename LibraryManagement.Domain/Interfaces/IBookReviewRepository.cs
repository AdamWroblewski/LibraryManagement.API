using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookReviewRepository : IRepository<BookReview>
    {
        Task<bool> HasReviewAsync(int bookId, int userId, CancellationToken cancellation = default);
        Task<BookReview?> GetByBookIdAndUserId(int bookId, int userId, CancellationToken cancellationToken = default);
    }
}
