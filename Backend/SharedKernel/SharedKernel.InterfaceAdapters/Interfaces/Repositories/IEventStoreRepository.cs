using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Domain.Event;
using SharedKernel.Domain.UniqueKey;
using SharedKernel.InterfaceAdapters.Dto;

namespace SharedKernel.InterfaceAdapters.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(StoreEvent @event);
        Task<IEnumerable<StoreEvent>> GetAsync(EventKey key);
    }
}
