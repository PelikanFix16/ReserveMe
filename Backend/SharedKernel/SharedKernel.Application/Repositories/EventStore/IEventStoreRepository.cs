using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Repositories.EventStore
{
    public interface IEventStoreRepository
    {
         Task Save(DomainEvent @event);
         Task<IEnumerable<DomainEvent>> Get(AggregateKey key);
         
    }
}