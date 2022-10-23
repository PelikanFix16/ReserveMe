using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Dto;
using SharedKernel.InterfaceAdapters.Interfaces.Events;
using SharedKernel.InterfaceAdapters.Interfaces.EventStore;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventStore;

namespace SharedKernel.InterfaceAdapters.Common.Events
{
    public class EventStoreManager : IEventStoreManager
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IStoreEventConverter _converter;

        public EventStoreManager(IEventStoreRepository eventStoreRepository, IStoreEventConverter converter)
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
