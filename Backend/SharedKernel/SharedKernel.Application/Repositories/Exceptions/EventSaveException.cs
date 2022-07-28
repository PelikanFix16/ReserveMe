namespace SharedKernel.Application.Repositories.Exceptions
{
    public class EventSaveException : Exception
    {
        public EventSaveException(string message)
            : base(message)
        {
        }

        public EventSaveException()
            : base()
        {
        }

        public EventSaveException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
