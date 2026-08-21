namespace LibraryManagement.Domain.Entities
{
    public class BookReview
    {
        public int Id { get; private set; }

        public Book Book { get; private set; }
        public int BookId { get; private set; }

        public int UserId { get; private set; }
        public string Comment { get; private set; }
        public int Rate { get; private set; }

        public BookReview(int bookId, int userId, int rate, string comment)
        {
            BookId = bookId;
            UserId = userId;
            Rate = rate;
            Comment = comment;
        }

        public void UpdateDetails(string comment, int rate)
        {
            Comment = comment;
            Rate = rate;
        }
    }
}