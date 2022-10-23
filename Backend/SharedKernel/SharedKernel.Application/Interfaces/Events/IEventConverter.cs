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
        public DomainEvent SharedEventToDomain(SharedEvent @event);
        public SharedEvent DomainEventToShared(DomainEvent @event);
    }
}
