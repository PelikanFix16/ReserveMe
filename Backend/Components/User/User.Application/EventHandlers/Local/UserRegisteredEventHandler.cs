using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Interfaces.Events;
using User.Domain.User.Events;

namespace User.Application.EventHandlers.Local
{
    public class UserRegisteredEventHandler : IEventHandle<UserRegisteredEvent>
    {
        public async Task HandleAsync(UserRegisteredEvent @event)
        {
            await Task.Delay(10000);
            Console.WriteLine(@event.Name.FirstName);
        }
    }
}
