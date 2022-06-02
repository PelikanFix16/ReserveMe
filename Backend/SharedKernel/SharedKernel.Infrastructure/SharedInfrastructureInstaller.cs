using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application;
using SharedKernel.Application.AggregateRepository;
using SharedKernel.Application.EventBus;
using SharedKernel.Infrastructure.EventRepository;
using SharedKernel.Infrastructure.EventsBus;
using SharedKernel.Infrastructure.EventStore;

namespace SharedKernel.Infrastructure
{
    public static class SharedInfrastructureInstaller
    {
        public static IServiceCollection AddEventStoreMongoDb(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddScoped<IMongoDbContext, MongoDbContext>();
            service.AddScoped<IEventStore, MongoDbEventStore>();
            return service;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddSingleton<IEventPublisher, EventPublisher>();
            return service;
        }

        public static IServiceCollection AddEventRepository(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddOptions();
            service.AddScoped<ISessionAggregate, SessionAggregate>();
            service.AddScoped<IEventRepository, EventRepositorySession>();
            return service;

        }

 

    }
}