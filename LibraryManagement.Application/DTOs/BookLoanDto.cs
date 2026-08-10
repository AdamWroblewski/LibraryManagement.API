namespace LibraryManagement.Application.DTOs
{
    public class BookLoanDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public LoanStatus Status { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}