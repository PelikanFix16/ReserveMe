using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;

namespace SharedKernel.InterfaceAdapters.EventsFlowController
{
    public class EventController : IEventController
    {
        private readonly IEventStoreRepository _eventRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public EventController(IEventStoreRepository eventRepository, IEventDispatcher eventDispatcher)
        {
            _eventRepository = eventRepository;
            _eventDispatcher = eventDispatcher;
        }

        public Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key) => _eventRepository.GetAsync(key);

        public async Task SaveAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                await _eventRepository.SaveAsync(@event);
                await _eventDispatcher.DispatchAsync(@event);
            }
        }
    }
}
