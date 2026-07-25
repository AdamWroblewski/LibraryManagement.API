namespace LibraryManagement.Application.CustomExceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName) : base($"{entityName} was not found.")
        {
        }
    }
}
