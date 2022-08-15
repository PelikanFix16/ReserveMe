using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using SharedKernel.Application;

namespace User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSharedKernelApplication();
            return services;
        }
    }
}
