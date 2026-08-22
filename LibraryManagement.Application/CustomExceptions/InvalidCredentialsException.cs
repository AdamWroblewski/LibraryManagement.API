namespace LibraryManagement.Application.CustomExceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid email or password.")
        {
        }
    }
}
