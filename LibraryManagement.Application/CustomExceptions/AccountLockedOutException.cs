namespace LibraryManagement.Application.CustomExceptions
{
    public class AccountLockedOutException : Exception
    {
        public AccountLockedOutException()
            : base("Account is temporarily locked due to multiple failed login attempts. Try again later.")
        {
        }
    }
}
