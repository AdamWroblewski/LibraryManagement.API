using LibraryManagement.Domain.CustomExceptions;

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
        public DateTime? CancelledAt { get; private set; }
        public DateTime HoldExpiresAt => ReservedAt.AddHours(ReservationHoldPolicyHours);


        public const int ReservationHoldPolicyHours = 72;
        public const int CheckoutPolicyDays = 28;

        public BookLoan(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
            Status = LoanStatus.Reserved;
            ReservedAt = DateTime.UtcNow;
        }

        public static BookLoan CreateDirectLoan(int bookId, int userId)
        {
            var loan = new BookLoan(bookId, userId)
            {
                Status = LoanStatus.Active,
                CheckedOutAt = DateTime.UtcNow,
                DueAt = DateTime.UtcNow.AddDays(CheckoutPolicyDays)
            };

            return loan;
        }

        public void MarkAsExpired()
        {
            if (Status != LoanStatus.Reserved)
                throw new InvalidLoanStatusTransitionException(
                    $"Cannot expire a loan with status '{Status}'. Only 'Reserved' loans can be expired.");

            Status = LoanStatus.Expired;
            ExpiredAt = DateTime.UtcNow;
        }

        public void MarkAsReturned()
        {
            if (Status != LoanStatus.Active && Status != LoanStatus.Overdue)
                throw new InvalidLoanStatusTransitionException(
                    $"Cannot return loan. Only 'Active' or 'Overdue' can be returned.");

            Status = LoanStatus.Returned;
            ReturnedAt = DateTime.UtcNow;
        }

        public void CancelReservation()
        {
            if (Status != LoanStatus.Reserved)
                throw new InvalidLoanStatusTransitionException(
                    $"Cannot cancel loan. Only 'Reserved' can be cancelled.");

            Status = LoanStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
        }

        public void Checkout()
        {
            if (Status != LoanStatus.Reserved)
                throw new InvalidLoanStatusTransitionException(
                    $"Cannot check out a loan with status '{Status}'. Only 'Reserved' loans can be checked out.");

            Status = LoanStatus.Active;
            CheckedOutAt = DateTime.UtcNow;
            DueAt = DateTime.UtcNow.AddDays(CheckoutPolicyDays);
        }
    }
}