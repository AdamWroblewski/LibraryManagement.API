using LibraryManagement.Application.Models;

namespace LibraryManagement.Application.DTOs
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public ICollection<BookLoanDto> CurrentUserLoans { get; set; } = new List<BookLoanDto>();
        public PagedList<BookReviewDto> Reviews { get; set; } = default!;
    }
}