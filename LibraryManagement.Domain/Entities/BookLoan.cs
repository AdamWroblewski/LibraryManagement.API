namespace LibraryManagement.Domain.Entities
{
    public class BookLoan
    {
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public Book? Book { get; private set; }
        public int UserId { get; private set; }

        public LoanStatus Status { get; private set; }
        public DateTime ReservedAt { get; private set; }
        public DateTime? CheckedOutAt { get; private set; }
        public DateTime? DueAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public DateTime? ExpiredAt { get; private set; }
        public DateTime HoldExpiresAt => ReservedAt.AddHours(HoldPolicyHours);


        public const double HoldPolicyHours = 72;

        public BookLoan(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
            Status = LoanStatus.Reserved;
            ReservedAt = DateTime.UtcNow;
        }

        public BookLoan(int bookId, int userId, LoanStatus status)
        {
            BookId = bookId;
            UserId = userId;
            Status = status;
            ReservedAt = DateTime.UtcNow;
            CheckedOutAt = status == LoanStatus.Active ? DateTime.UtcNow : null;
        }

        public void MarkAsExpired()
        {
            if (Status != LoanStatus.Reserved)
            {
                throw new InvalidOperationException($"Cannot expire a loan with status '{Status}'. Only 'Reserved' loans can be expired.");
            }

            Status = LoanStatus.Expired;
            ExpiredAt = DateTime.UtcNow;
        }
    }
}