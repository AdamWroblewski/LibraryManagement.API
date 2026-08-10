public enum LoanStatus
{
    Reserved,   // Waiting for user pickup
    Active,     // Successfully picked up / checked out
    Returned,   // Completed loan
    Overdue,    // Active loan past due date
    Expired,    // Hold period elapsed without pickup
    Cancelled   // Cancelled manually by user or librarian
}