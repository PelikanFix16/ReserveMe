using System.Collections.Generic;

namespace SharedKernel.Application
{
    public interface IEventStore
    {
         void Save(DomainEvent @event);
         List<DomainEvent> Get(AggregateKey aggregateKey,int fromVersion);
    }
}