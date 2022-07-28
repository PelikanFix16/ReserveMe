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

        public Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key)
        {
            return _eventStore.GetAsync(key);
        }

        public async Task SaveAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                await _eventStore.SaveAsync(@event);
                _eventPublisher.Publish(@event);
            }
        }
    }
}
