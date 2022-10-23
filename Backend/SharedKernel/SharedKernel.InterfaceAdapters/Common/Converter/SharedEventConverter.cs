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
                EventName = @event.GetType().Namespace + "." + @event.GetType().Name,
                EventData = JsonConvert.SerializeObject(@event),
                AssemblyName = assembly
            };
        }

        public DomainEvent SharedEventToDomain(SharedEvent @event)
        {
            var qualified = Assembly.CreateQualifiedName(@event.AssemblyName, @event.EventName);
            var elementType = Type.GetType(qualified);
            if (elementType is null)
                throw new ArgumentException($"Type {@event.EventName} not found");

            var domainObj = JsonConvert.DeserializeObject(@event.EventData, elementType);
            if (domainObj is null)
                throw new Exception("Could not convert store event to domain event");

            if (domainObj is not DomainEvent obj)
                throw new Exception("Could not convert store event to domain event");

            return obj;
        }
    }
}
