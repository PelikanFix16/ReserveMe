using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Interfaces.Converter;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;

namespace SharedKernel.InterfaceAdapters.EventsFlowController
{
    public class EventStoreManagerRepositories : IEventStoreManagerRepositories
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IStoreEventConverter _converter;

        public EventStoreManagerRepositories(IEventStoreRepository eventStoreRepository, IStoreEventConverter converter)
        {
            _eventStoreRepository = eventStoreRepository;
            _converter = converter;
        }

        public async Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key)
        {
            var eventKey = _converter.AggregateKeyToEventKey(key);
            var eventFromStore = await _eventStoreRepository.GetAsync(eventKey);
            return eventFromStore.Select(e => _converter.StoreToDomainEvent(e));
        }

        public async Task SaveAsync(DomainEvent @event)
        {
            await _eventStoreRepository.SaveAsync(_converter.DomainToStoreEvent(@event));
        }
    }
}
