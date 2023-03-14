using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure;
using User.Application.Interfaces.Repositories;
using User.Application.Interfaces.Security;
using User.Infrastructure.Persistence.DataContext;
using User.Infrastructure.Persistence.Repositories;
using User.Infrastructure.Security;

namespace User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddSingleton<ISecurityHash,SecurityHash>();
            services.AddSharedKernelInfrastructure(configuration);
            services.AddDbContext<UserContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        configuration.GetConnectionString("User"),
                        new MySqlServerVersion(new Version(8,0,29)))
            );
            services.AddDbContext<ManagerContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        configuration.GetConnectionString("User"),
                        new MySqlServerVersion(new Version(8,0,29)))
            );
            services.AddTransient<IUserProjectionRepository,UserRepository>();
            services.AddTransient<IManagerProjectionRepository,ManagerRepository>();
            return services;
        }
    }
}
