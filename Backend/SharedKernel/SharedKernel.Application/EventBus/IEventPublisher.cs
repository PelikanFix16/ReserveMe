namespace SharedKernel.Application.EventBus
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : DomainEvent; 
    }
}