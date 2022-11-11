using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Behaviors;
using SharedKernel.Application.Interfaces.Events;
using SharedKernel.Application.LocalEventPublisher;

namespace SharedKernel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelApplication(
            this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatRPipelineValidation<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatRPipelineException<,>));
            services.AddScoped<IEventPublish, EventPublisher>();
            return services;
        }
    }
}
