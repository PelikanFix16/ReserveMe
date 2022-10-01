using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Common.Interfaces.Security;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;
using SharedKernel.Infrastructure.Security;
using SharedKernel.InterfaceAdapters;
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
            services.AddSharedKernelInterfaceAdapters();
            services.AddSingleton<IPasswordHash, PasswordHash>();
            return services;
        }
    }
}
