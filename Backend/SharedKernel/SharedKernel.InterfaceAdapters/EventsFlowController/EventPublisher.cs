using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;

namespace SharedKernel.InterfaceAdapters.EventsFlowController
{
    public class EventPublisher : IEventPublish
    {
        private readonly IEnumerable<IEventHandleBase> _handlers;

        public EventPublisher(IEnumerable<IEventHandleBase> handlers)
        {
            _handlers = handlers;
        }

        public async Task PublishAsync(DomainEvent @event)
        {
            foreach (var handler in _handlers)
            {
                var handleMethod = handler.GetType().GetMethod(nameof(IEventHandle<DomainEvent>.HandleAsync));
                if (handleMethod is null)
                    continue;

                var taskInvoke = handleMethod.Invoke(handler, new[] { @event }) as Task ?? Task.CompletedTask;
                await taskInvoke;
            }
        }
    }
}
