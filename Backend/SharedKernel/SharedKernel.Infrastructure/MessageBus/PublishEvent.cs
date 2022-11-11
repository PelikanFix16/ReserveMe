using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using SharedKernel.Application.Common.Event;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.MessageBus
{
    public class PublishEvent : IEventPublish
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IEventConverter _converter;

        private readonly bool _notEnableMessageBus;


        public PublishEvent(IEventConverter converter, IPublishEndpoint publishEndpoint = null)
        {
            if (publishEndpoint is null)
                _notEnableMessageBus = true;

            _publishEndpoint = publishEndpoint;
            _converter = converter;
        }

        public async Task PublishAsync(SharedEvent @event)
        {
            if (_notEnableMessageBus)
                return;

            var method = typeof(IEventConverter).GetMethod(nameof(IEventConverter.SharedEventToDomain));
            var generic = method.MakeGenericMethod(@event.EventTypeName);
            var domainEvent = generic.Invoke(_converter, new object[] { @event });
            await _publishEndpoint.Publish(domainEvent);
        }
    }
}
