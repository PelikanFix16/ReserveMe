using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Interfaces.Repositories;
using SharedKernel.InterfaceAdapters.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Repositories;

namespace SharedKernel.InterfaceAdapters
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelInterfaceAdapters(
            this IServiceCollection services)
        {
            services.AddTransient<IEventDispatcher,EventDispatcher>();
            services.AddTransient<IEventController,EventController>();
            services.AddTransient<IAggregateRepository,AggregateRepository>();
            return services;
        }
    }
}
