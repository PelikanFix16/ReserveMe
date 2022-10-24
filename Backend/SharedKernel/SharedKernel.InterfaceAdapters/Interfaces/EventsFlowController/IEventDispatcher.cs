using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;

namespace SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(DomainEvent @event);
    }
}
