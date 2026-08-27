namespace LibraryManagement.Domain.CustomExceptions
{
    public class InvalidLoanStatusTransitionException : Exception
    {
        public InvalidLoanStatusTransitionException(string message) : base(message)
        {
        }
    }
}
