namespace LibraryManagement.Application.DTOs
{
    public class ActiveBookLoanDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool IsReservation { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}