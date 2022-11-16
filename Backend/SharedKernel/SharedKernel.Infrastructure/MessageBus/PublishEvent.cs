using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using SharedKernel.Domain.Event;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;

namespace SharedKernel.Infrastructure.MessageBus
{
    public class PublishEvent : IEventPublish
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly bool _notEnableMessageBus;

        public PublishEvent(IPublishEndpoint publishEndpoint = null)
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
