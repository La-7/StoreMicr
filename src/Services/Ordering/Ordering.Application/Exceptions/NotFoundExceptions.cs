namespace Ordering.Application.Exceptions
{
    // TODO: update some aspects of error handling
    public class NotFoundExceptions : ApplicationException
    {
        public NotFoundExceptions(string name, object key) 
            : base($"Entity \'{name}\' ({key}) was not found")
        {
        }
    }
}
