using SharedKernel.Domain.Event;

namespace SharedKernel.Application.Repositories.EventBus
{
    public interface IPublishEvent
    {
        void Publish(DomainEvent @event);
    }
}
