using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;

namespace SharedKernel.Infrastructure.Messages.Internal
{
    public class PublishEventInternal : IEventPublish
    {
        private readonly IEnumerable<IEventHandleBase> _handlers;

        public PublishEventInternal(IEnumerable<IEventHandleBase> handlers) => _handlers = handlers;

        public async Task PublishAsync(DomainEvent @event)
        {
            foreach (IEventHandleBase handler in _handlers)
            {
                System.Reflection.MethodInfo handleMethod = handler.GetType()
                    .GetMethod(nameof(IEventHandle<DomainEvent>.HandleAsync));
                if (handleMethod is null)
                    continue;

                if (handleMethod.GetParameters().First().ParameterType != @event.GetType())
                    continue;

                Task taskInvoke = handleMethod.Invoke(handler,new[]
                {
                    @event
                }) as Task ?? Task.CompletedTask;
                await taskInvoke;
            }
        }
    }
}