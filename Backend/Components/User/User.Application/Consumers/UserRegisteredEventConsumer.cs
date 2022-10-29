using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;
using User.Domain.User.Events;

namespace User.Application.Consumers
{
    public class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
    {

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            Console.WriteLine($"Event received: {context.Message.Name.FirstName}");
        }
    }
}
