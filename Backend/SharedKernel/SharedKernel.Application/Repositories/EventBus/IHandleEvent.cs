using SharedKernel.Domain.Event;

namespace SharedKernel.Application.Repositories.EventBus
{
    public interface IHandleEvent
    {
        Task HandleAsync(DomainEvent @event);
    }
}
