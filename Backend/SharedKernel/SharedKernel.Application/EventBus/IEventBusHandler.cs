using System;

namespace SharedKernel.Application.EventBus
{
    public interface IEventBusHandler
    {
         Type HandlerType{get;}
         void Handle(DomainEvent @event);
    }
}