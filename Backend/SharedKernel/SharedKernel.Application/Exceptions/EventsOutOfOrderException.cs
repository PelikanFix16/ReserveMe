using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Exceptions
{
    public class EventsOutOfOrderException : Exception
    {
        public EventsOutOfOrderException(AggregateKey key) : 
            base($"Eventstore gave event for aggregate {key} out of order")
        {
            
        }
    }
}