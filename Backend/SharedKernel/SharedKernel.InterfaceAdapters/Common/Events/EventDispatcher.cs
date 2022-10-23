using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Interfaces.Events;

namespace SharedKernel.InterfaceAdapters.Common.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEnumerable<IEventPublish> _eventsPublisher;
        private readonly IEventConverter _eventConverter;

        public EventDispatcher(IEnumerable<IEventPublish> eventsPublisher, IEventConverter eventConverter)
        {
            _eventsPublisher = eventsPublisher;
            _eventConverter = eventConverter;
        }

        public async Task DispatchAsync(DomainEvent @event)
        {
            foreach (var eventsPublisher in _eventsPublisher)
            {
                await eventsPublisher.PublishAsync(_eventConverter.DomainEventToShared(@event));
            }
        }
    }
}
