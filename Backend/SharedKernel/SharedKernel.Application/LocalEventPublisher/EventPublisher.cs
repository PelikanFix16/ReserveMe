using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Common.Event;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain.Event;

namespace SharedKernel.Application.LocalEventPublisher
{
    public class EventPublisher : IEventPublish
    {
        private readonly IEventConverter _converter;
        private readonly IEnumerable<IEventHandleBase> _handlers;

        public EventPublisher(IEventConverter converter, IEnumerable<IEventHandleBase> handlers)
        {
            _converter = converter;
            _handlers = handlers;
        }

        public async Task PublishAsync(SharedEvent @event)
        {
            foreach (var handler in _handlers)
            {
                var method = typeof(IEventConverter).GetMethod(nameof(IEventConverter.SharedEventToDomain));
                if (method is null)
                    continue;

                var generic = method.MakeGenericMethod(@event.EventTypeName);
                if (generic.Invoke(_converter, new object[] { @event }) is not DomainEvent domainEvent)
                    continue;

                var eventHandler = Array.Find(
                    handler.GetType()
                        .GetInterfaces(),
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandle<>));
                if (eventHandler == null)
                    continue;

                var eventType = eventHandler.GetGenericArguments()[0];
                if (eventType != domainEvent.GetType())
                    continue;

                var handleMethod = handler.GetType().GetMethod(nameof(IEventHandle<DomainEvent>.HandleAsync));
                if (handleMethod is null)
                    continue;

                await (Task)handleMethod.Invoke(handler, new object[] { domainEvent });
            }
        }
    }

}
