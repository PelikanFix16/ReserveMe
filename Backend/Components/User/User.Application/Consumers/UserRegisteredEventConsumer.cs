using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;

namespace User.Application.Consumers
{
    public class UserRegisteredEventConsumer : IConsumer<SharedEvent>
    {
        public const string UserRegisteredEventName = nameof(EventConstants.USER_REGISTERED);

        public async Task Consume(ConsumeContext<SharedEvent> context)
        {
            Console.WriteLine($"Event received: {context.Message.EventName}");
        }
    }
}
