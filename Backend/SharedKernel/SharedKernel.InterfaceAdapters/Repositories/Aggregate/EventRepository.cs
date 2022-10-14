using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Common.Converter;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventBus;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventStore;

namespace SharedKernel.SharedKernel.InterfaceAdapters.Repositories.Aggregate
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

        public async Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key)
        {
            //map aggregate key to event key class
            // pass event key class to get async
            // map store event to domain event
            // return domain event
            var eventKey = EventConverter.DomainToStoreEventKey(key);
            var eventFromStore = await _eventStore.GetAsync(eventKey);
            var x = eventFromStore.Select(e => EventConverter.StoreToDomainEvent(e));
            return x;
        }

        public async Task SaveAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                // map event to store event
                await _eventStore.SaveAsync(EventConverter.DomainToStoreEvent(@event));
                await _eventPublisher.PublishAsync(EventConverter.DomainToSharedEvent(@event));
            }
        }
    }

}
