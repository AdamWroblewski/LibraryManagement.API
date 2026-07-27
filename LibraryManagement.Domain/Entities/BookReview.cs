namespace LibraryManagement.Domain.Entities
{
    public class BookReview
    {
        public int Id { get; private set; }

        public Book Book { get; private set; }
        public int BookId { get; private set; }

        public int ApplicationUserId { get; private set; }
        public string Comment { get; private set; }
        public int Rate { get; private set; }

        public BookReview(int bookId, int applicationUserId, int rate, string comment)
        {
            BookId = bookId;
            ApplicationUserId = applicationUserId;
            Rate = rate;
            UpdateComment(comment);
        }

        public void UpdateDetails(int userId, string comment, int rate)
        {
            Comment = comment;
            Rate = rate;
        }

        private void UpdateComment(string comment)
        {
            Comment = comment ?? string.Empty;
        }
    }
}