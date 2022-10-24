using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Application.Interfaces.Repositories;
using SharedKernel.InterfaceAdapters.Common.Converter;
using SharedKernel.InterfaceAdapters.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.Converter;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Repositories;

namespace SharedKernel.InterfaceAdapters
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelInterfaceAdapters(
            this IServiceCollection services)
        {
            services.AddTransient<IEventConverter, SharedEventConverter>();
            services.AddTransient<IStoreEventConverter, StoreEventConverter>();
            services.AddTransient<IEventStoreManagerRepositories, EventStoreManagerRepositories>();
            services.AddTransient<IEventDispatcher, EventDispatcher>();
            services.AddTransient<IEventController, EventController>();
            services.AddTransient<IAggregateRepository, AggregateRepository>();

            return services;
        }
    }
}
