using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Application.Common.Event;

namespace SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventBus
{
    public interface IPublishEvent
    {
        Task PublishAsync(SharedEvent @event);
    }
}
