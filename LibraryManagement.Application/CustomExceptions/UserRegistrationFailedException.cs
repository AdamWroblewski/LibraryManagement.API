namespace LibraryManagement.Application.CustomExceptions
{
    public class UserRegistrationFailedException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public UserRegistrationFailedException(IEnumerable<string> errors)
            : base("User registration failed.")
        {
            Errors = errors.ToList();
        }
    }
}
