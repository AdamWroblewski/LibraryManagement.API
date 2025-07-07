namespace LibraryManagement.Domain.Entities
{
    public class BookLoan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ApplicationUserId { get; set; }
        public bool IsReservation { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}