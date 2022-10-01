using SharedKernel.Domain.Event;

namespace SharedKernel.Application.Repositories.EventBus
{
    public interface IPublishEvent
    {
        Task PublishAsync(DomainEvent @event);
    }
}
