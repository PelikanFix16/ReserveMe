using SharedKernel.Domain.Event;

namespace SharedKernel.Domain.Event
{
    public static class EventHelper
    {
        public static string SelectQueue(this DomainEvent @event)
        {
            string name = @event.GetType().Name;


            string g = name switch
            {
                EventConstants.USER_REGISTERED => nameof(EventConstants.USER_REGISTERED),
                _ => throw new NotImplementedException(),
            };
            return g;
        }
    }
}