using SharedKernel.Domain.Constants;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Domain.Event
{
    public abstract class DomainEvent : IEvent
    {
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public AggregateKey Key { get; protected set; }


        protected DomainEvent(AggregateKey key, int version)
        {
            TimeStamp = AppTime.Now();
            Version = version;
            Key = key;

        }
    }
}