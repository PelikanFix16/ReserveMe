using System.Reflection;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.EventsBus
{
      public static class EventHelper
    {
        public static string SelectQueue(this DomainEvent @event)
        {
            string name = @event.GetType().Name;

            var availableEvents = typeof(EventConstants).GetFields(BindingFlags.Public | 
                                            BindingFlags.Static |
                                            BindingFlags.FlattenHierarchy)
                                            .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            foreach(var propertyEvent in availableEvents){
                if((propertyEvent.GetRawConstantValue() as string) == name){
                    return propertyEvent.Name;
                }
            }

            throw new NotImplementedException();


        }
    }
}