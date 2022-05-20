using System.Collections.Generic;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application
{
    public interface IEventStore
    {
         void Save(DomainEvent @event);
         List<DomainEvent> Get(AggregateKey aggregateKey,int fromVersion);
    }
}