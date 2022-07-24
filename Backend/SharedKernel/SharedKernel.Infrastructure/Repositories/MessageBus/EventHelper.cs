using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.Repositories.MessageBus
{
    public static class EventHelper
    {
        public static string SelectQueue(this DomainEvent @event)
        {
            var name = @event.GetType().Name;

            var availableEvents = typeof(EventConstants).GetFields(BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .ToList();

            foreach (var propertyEvent in availableEvents)
            {
                if ((propertyEvent.GetRawConstantValue() as string) == name)
                {
                    return propertyEvent.Name;
                }
            }

            return "NOT_FOUND";


        }
    }
}
