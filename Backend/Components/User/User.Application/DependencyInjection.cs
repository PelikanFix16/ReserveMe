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
using User.Application.EventHandlers.Local.User;
using User.Application.EventHandlers.Local.Manager;
using User.Application.EventHandlers.Local.Employee;

namespace User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IEventHandleBase,UserRegisteredEventHandler>();
            services.AddTransient<IEventHandleBase,UserRegisteredConfirmationEventHandler>();
            services.AddTransient<IEventHandleBase,ManagerCreatedEventHandler>();
            services.AddTransient<IEventHandleBase,EmployeeCreatedEventHandler>();
            services.AddTransient<IManagerService,ManagerService>();
            services.AddTransient<IEmployeeService,EmployeeService>();
            services.AddSharedKernelApplication();
            return services;
        }
    }
}
