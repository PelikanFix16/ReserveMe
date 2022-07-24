using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.Infrastructure.Repositories.MessageBus.Rabbit;

namespace Infrastructure.Test.EventBus.Rabbit
{
    public static class RabbitSettingsLocal
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";

        public static RabbitSettings GetConfig()
        {
            return new RabbitSettings
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };
        }

    }
}
