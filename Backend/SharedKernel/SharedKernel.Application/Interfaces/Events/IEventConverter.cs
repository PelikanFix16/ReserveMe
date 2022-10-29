using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Common.Event;
using SharedKernel.Domain.Event;

namespace SharedKernel.Application.Interfaces.Events
{
    public interface IEventConverter
    {
        //We are going to create concrete type for each event, like ... : DomainEvent
        // To invoke this method we need to use reflection when it is a shared kernel
        // Because Shared Kernel doesn't know about events in components
        public T SharedEventToDomain<T>(SharedEvent @event) where T : DomainEvent;
        public SharedEvent DomainEventToShared(DomainEvent @event);
    }
}
