using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;
using SharedKernel.Infrastructure.Repositories.MessageBus;
using SharedKernel.InterfaceAdapters;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventBus;
using SharedKernel.SharedKernel.InterfaceAdapters.Interfaces.EventStore;

namespace SharedKernel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.Configure<MongoSettings>(configurationManager.GetSection(MongoSettings.SectionName));

            services.AddSingleton<IEventStoreRepository, MongoEventStore>();
            services.AddTransient<IPublishEvent, PublishEvent>();
            services.AddSharedKernelInterfaceAdapters();
            return services;
        }
    }
}
