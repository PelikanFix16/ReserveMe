using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Repositories.EventBus;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.Repositories.MessageBus
{
    public class PublishEvent : IPublishEvent
    {
        public Task PublishAsync(DomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
