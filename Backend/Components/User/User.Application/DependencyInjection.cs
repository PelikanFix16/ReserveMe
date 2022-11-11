using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using SharedKernel.Application;
using FluentValidation;
using User.Application.Interfaces.Services;
using User.Application.Services;
using SharedKernel.Application.Interfaces.Events;
using User.Application.EventHandlers.Local;

namespace User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEventHandleBase, UserRegisteredEventHandler>();
            services.AddSharedKernelApplication();
            return services;
        }
    }
}
