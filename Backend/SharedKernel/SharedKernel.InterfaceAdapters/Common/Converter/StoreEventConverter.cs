using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Dto;
using SharedKernel.InterfaceAdapters.Interfaces.Events;

namespace SharedKernel.InterfaceAdapters.Common.Converter
{
    public class StoreEventConverter : IStoreEventConverter
    {
        public EventKey AggregateKeyToEventKey(AggregateKey key)
        {
            return new EventKey()
            {
                Key = key.Key
            };
        }

        public StoreEvent DomainToStoreEvent(DomainEvent domainEvent)
        {
            var assembly = domainEvent.GetType().Assembly.GetName().Name ?? "";
            return new StoreEvent()
            {
                EventId = domainEvent.Key.Key,
                EventName = domainEvent.GetType().Namespace + "." + domainEvent.GetType().Name,
                EventData = JsonConvert.SerializeObject(domainEvent),
                EventDate = domainEvent.TimeStamp,
                Version = domainEvent.Version,
                AssemblyName = assembly
            };
        }

        public DomainEvent StoreToDomainEvent(StoreEvent storeEvent)
        {
            var qualified = Assembly.CreateQualifiedName(storeEvent.AssemblyName, storeEvent.EventName);
            var elementType = Type.GetType(qualified);
            if (elementType is null)
                throw new ArgumentException($"Type {storeEvent.EventName} not found");

            var domainObj = JsonConvert.DeserializeObject(storeEvent.EventData, elementType);
            if (domainObj is null)
                throw new Exception("Could not convert store event to domain event");

            if (domainObj is not DomainEvent obj)
                throw new Exception("Could not convert store event to domain event");

            return obj;
        }
    }
}
