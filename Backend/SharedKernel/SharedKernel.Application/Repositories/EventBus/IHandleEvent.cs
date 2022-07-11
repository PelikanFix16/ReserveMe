using SharedKernel.Domain.Event;

namespace SharedKernel.Application.Repositories.EventBus
{
    public interface IHandleEvent
    {
         Task Handle(DomainEvent @event);
    }
}