using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(AggregateKey key) : base(string.Format("Aggregate {0} not found", key))
        {

        }
    }
}