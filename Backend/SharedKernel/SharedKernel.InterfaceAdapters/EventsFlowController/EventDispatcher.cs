using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;

namespace SharedKernel.InterfaceAdapters.EventsFlowController
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEnumerable<IEventPublish> _eventsPublisher;

        public EventDispatcher(IEnumerable<IEventPublish> eventsPublisher) => _eventsPublisher = eventsPublisher;

        public async Task DispatchAsync(DomainEvent @event)
        {
            foreach (IEventPublish eventsPublisher in _eventsPublisher)
            {
                await eventsPublisher.PublishAsync(@event);
            }
        }
    }
}
