using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
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

        public Task<IEnumerable<DomainEvent>> GetAsync(AggregateKey key)
        {
            return _eventStore.GetAsync(key);
        }

        public async Task SaveAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                await _eventStore.SaveAsync(@event);
                await _eventPublisher.PublishAsync(ToSharedEvent(@event));
            }
        }

        private static SharedEvent ToSharedEvent(DomainEvent @event)
        {
            return new SharedEvent()
            {
                EventName = nameof(@event),
                EventData = JsonConvert.SerializeObject(@event)
            };
        }
    }

}
