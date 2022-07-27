namespace SharedKernel.Application.Repositories.Exceptions
{
    public class EventSaveException : Exception
    {
        public EventSaveException(string message)
            : base(message)
        {
        }
    }
}
