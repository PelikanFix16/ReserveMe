using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using SharedKernel.Application.Common.Event;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventBus;

namespace SharedKernel.Infrastructure.Repositories.MessageBus
{
    public class PublishEvent : IPublishEvent
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishEvent(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(SharedEvent @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}
