using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Common.Interfaces.Security;
using SharedKernel.Application.Repositories.Aggregate;
using SharedKernel.Application.Repositories.EventBus;
using SharedKernel.Application.Repositories.EventStore;
using SharedKernel.Infrastructure.Repositories.Aggregate;
using SharedKernel.Infrastructure.Repositories.EventStore.Mongo;
using SharedKernel.Infrastructure.Repositories.MessageBus.Rabbit;
using SharedKernel.Infrastructure.Security;

namespace SharedKernel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.Configure<MongoSettings>(configurationManager.GetSection(MongoSettings.SectionName));
            services.Configure<RabbitSettings>(configurationManager.GetSection(RabbitSettings.SectionName));
            services.AddSingleton<IEventStoreRepository, MongoEventStore>();
            services.AddSingleton<IPublishEvent, PublishEvent>();
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IAggregateRepository, AggregateRepository>();
            services.AddSingleton<IPasswordHash, PasswordHash>();
            return services;
        }
    }
}
