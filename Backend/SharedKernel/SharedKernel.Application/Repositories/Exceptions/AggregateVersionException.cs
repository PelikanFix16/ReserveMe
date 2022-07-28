namespace SharedKernel.Application.Repositories.Exceptions
{
    public class AggregateVersionException : Exception
    {
        public AggregateVersionException(string message)
            : base(message)
        {
        }

        public AggregateVersionException()
            : base()
        {
        }

        public AggregateVersionException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
