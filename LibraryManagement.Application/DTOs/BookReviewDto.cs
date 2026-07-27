namespace LibraryManagement.Application.DTOs
{
    public class BookReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public int Rate { get; set; }
    }
}