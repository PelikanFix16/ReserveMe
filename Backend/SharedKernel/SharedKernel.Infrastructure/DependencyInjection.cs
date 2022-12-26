using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Interfaces.Notification;
using SharedKernel.Infrastructure.EventStore;
using SharedKernel.Infrastructure.MessageBus;
using SharedKernel.Infrastructure.Notification;
using SharedKernel.InterfaceAdapters;
using SharedKernel.InterfaceAdapters.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.EventsFlowController;
using SharedKernel.InterfaceAdapters.Interfaces.Repositories;

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
            services.AddTransient<IEventPublish, PublishEvent>();
            services.AddTransient<IEmailNotification, EmailNotification>();
            services.AddSharedKernelInterfaceAdapters();
            return services;
        }
    }
}
