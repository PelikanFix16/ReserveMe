using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Application.Interfaces.Repositories;
using SharedKernel.InterfaceAdapters.Common.Converter;
using SharedKernel.InterfaceAdapters.Common.Events;
using SharedKernel.InterfaceAdapters.Interfaces.Events;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.InterfaceAdapters.Repositories.Event;
using SharedKernel.SharedKernel.InterfaceAdapters.Repositories.Aggregate;

namespace SharedKernel.InterfaceAdapters
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelInterfaceAdapters(
            this IServiceCollection services)
        {
            services.AddTransient<IEventConverter, SharedEventConverter>();
            services.AddTransient<IStoreEventConverter, StoreEventConverter>();
            services.AddTransient<IEventDispatcher, EventDispatcher>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IAggregateRepository, AggregateRepository>();

            return services;
        }
    }
}
