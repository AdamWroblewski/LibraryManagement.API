namespace LibraryManagement.Application.DTOs
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<BookLoanDto> CurrentUserLoans { get; set; } = new List<BookLoanDto>();
        public ICollection<BookReviewDto> Reviews { get; set; } = new List<BookReviewDto>();
    }
}