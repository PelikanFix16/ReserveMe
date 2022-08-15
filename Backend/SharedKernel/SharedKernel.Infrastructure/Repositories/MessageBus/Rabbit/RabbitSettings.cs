using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Infrastructure.Repositories.MessageBus.Rabbit
{
    public class RabbitSettings
    {
        public const string SectionName = "RabbitSettings";

        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
