using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure;
using User.Application.Consumers;
using User.Application.Interfaces.Security;
using User.Infrastructure.Security;

namespace User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, rmqCfg) =>
                {
                    rmqCfg.ReceiveEndpoint(
                        UserRegisteredEventConsumer.UserRegisteredEventName,
                        e => e.Consumer<UserRegisteredEventConsumer>(context));
                });
            });
            services.AddScoped<UserRegisteredEventConsumer>();
            services.AddSingleton<ISecurityHash, SecurityHash>();
            services.AddSharedKernelInfrastructure(configuration);

            return services;
        }
    }
}
