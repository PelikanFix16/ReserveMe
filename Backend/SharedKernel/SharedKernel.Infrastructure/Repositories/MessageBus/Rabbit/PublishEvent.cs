using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedKernel.Application.Repositories.EventBus;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.Repositories.MessageBus.Rabbit
{
    public class PublishEvent : IPublishEvent
    {
        private readonly ConnectionFactory _factory;

        public PublishEvent(IOptions<RabbitSettings> settings)
        {
            var settingsRabbit = settings.Value;
            _factory = new ConnectionFactory()
            {
                HostName = settingsRabbit.HostName,
                UserName = settingsRabbit.UserName,
                Password = settingsRabbit.Password
            };
        }

        public void Publish(DomainEvent @event)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            var eventName = @event.SelectQueue();
            channel.QueueDeclare(eventName, true, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish("", eventName, properties, body);
        }
    }
}
