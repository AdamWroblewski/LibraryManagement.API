namespace LibraryManagement.Application.CustomExceptions
{
    public class DuplicateResourceException : Exception
    {
        public DuplicateResourceException(string message) : base(message) { }
    }
}
