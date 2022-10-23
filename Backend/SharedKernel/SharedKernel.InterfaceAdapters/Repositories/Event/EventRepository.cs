using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.Events;
using SharedKernel.InterfaceAdapters.Interfaces.EventStore;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;

namespace SharedKernel.InterfaceAdapters.Repositories.Event
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStoreManager _eventStoreManager;
        private readonly IEventDispatcher _eventDispatcher;

        public EventRepository(IEventStoreManager eventStoreManager, IEventDispatcher eventDispatcher)
        {
            _eventStoreManager = eventStoreManager;
            _eventDispatcher = eventDispatcher;
        }

        public Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key) => _eventStoreManager.GetAsync(key);

        public async Task SaveAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                await _eventStoreManager.SaveAsync(@event);
                await _eventDispatcher.DispatchAsync(@event);
            }
        }
    }
}
