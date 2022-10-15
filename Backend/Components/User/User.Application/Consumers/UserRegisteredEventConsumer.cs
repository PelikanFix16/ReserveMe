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

        public async Task Consume(ConsumeContext<SharedEvent> context)
        {
            Console.WriteLine($"Event received: {context.Message.EventData}");
        }
    }
}
