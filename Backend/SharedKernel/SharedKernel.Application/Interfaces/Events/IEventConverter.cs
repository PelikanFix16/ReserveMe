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
        public T SharedEventToDomain<T>(SharedEvent @event) where T : DomainEvent;
        public SharedEvent DomainEventToShared(DomainEvent @event);
    }
}
