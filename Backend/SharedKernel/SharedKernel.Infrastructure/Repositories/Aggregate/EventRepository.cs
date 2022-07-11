using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Application.Repositories.EventBus;
using SharedKernel.Application.Repositories.EventStore;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Infrastructure.Repositories.Aggregate
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly IPublishEvent _eventPublisher;
        public EventRepository(IEventStoreRepository eventStore, IPublishEvent eventPublisher)
        {
            _eventPublisher = eventPublisher;
            _eventStore = eventStore;
        }
        public async Task<IEnumerable<DomainEvent>> Get(AggregateKey key)
        {
            return await _eventStore.Get(key);
        }

        public async Task Save(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                await _eventStore.Save(@event);
                await _eventPublisher.Publish(@event);
            }

        }
    }
}