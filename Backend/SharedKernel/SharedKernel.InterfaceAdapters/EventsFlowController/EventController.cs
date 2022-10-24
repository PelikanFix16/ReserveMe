using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;


namespace SharedKernel.InterfaceAdapters.EventsFlowController
{
    public class EventController : IEventController
    {
        private readonly IEventStoreManagerRepositories _eventStoreManager;
        private readonly IEventDispatcher _eventDispatcher;

        public EventController(IEventStoreManagerRepositories eventStoreManager, IEventDispatcher eventDispatcher)
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
