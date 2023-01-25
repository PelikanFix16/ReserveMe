using MassTransit;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;

namespace SharedKernel.Infrastructure.Messages.MassTransit
{
    public class PublishEventMassTransit : IEventPublish
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly bool _notEnableMessageBus;

        public PublishEventMassTransit(IPublishEndpoint publishEndpoint = null)
        {
            if (publishEndpoint is null)
                _notEnableMessageBus = true;

            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(DomainEvent @event)
        {
            if (_notEnableMessageBus)
                return;

            await _publishEndpoint.Publish(@event);
        }
    }
}