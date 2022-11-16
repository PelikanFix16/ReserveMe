using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;

namespace SharedKernel.Application.Interfaces.Events
{
    public interface IEventHandle<T> : IEventHandleBase where T : DomainEvent
    {
        Task HandleAsync(T @event);
    }
}
