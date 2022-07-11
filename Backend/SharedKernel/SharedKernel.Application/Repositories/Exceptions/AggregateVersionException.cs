namespace SharedKernel.Application.Repositories.Exceptions
{
    public class AggregateVersionException : Exception
    {
        public AggregateVersionException(string message) : base(message)
        {

        }
    }
}