using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Behaviors;

namespace SharedKernel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelApplication(
            this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatRPipelineValidation<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatRPipelineException<,>));

            return services;
        }
    }
}
