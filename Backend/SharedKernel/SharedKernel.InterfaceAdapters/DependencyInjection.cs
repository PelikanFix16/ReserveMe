using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;
using SharedKernel.SharedKernel.InterfaceAdapters.Repositories.Aggregate;

namespace SharedKernel.InterfaceAdapters
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelInterfaceAdapters(
            this IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IAggregateRepository, AggregateRepository>();
            return services;
        }
    }
}
