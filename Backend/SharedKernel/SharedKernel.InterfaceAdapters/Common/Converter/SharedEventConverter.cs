using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedKernel.Application.Common.Event;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Domain.Event;

namespace SharedKernel.InterfaceAdapters.Common.Converter
{
    public class SharedEventConverter : IEventConverter
    {
        public SharedEvent DomainEventToShared(DomainEvent @event)
        {
            var assembly = @event.GetType().Assembly.GetName().Name ?? "";
            return new SharedEvent()
            {
                EventName = @event.GetType().Name,
                EventData = JsonConvert.SerializeObject(@event),
                AssemblyName = assembly
            };
        }

        public T SharedEventToDomain<T>(SharedEvent @event) where T : DomainEvent
        {
            var domainObj = JsonConvert.DeserializeObject<T>(@event.EventData);
            if (domainObj is null)
                throw new Exception("Could not convert store event to domain event");

            return domainObj;
        }
    }
}
