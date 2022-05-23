using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedKernel.Application.EventBus;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.EventsBus
{
    public abstract class EventPublisher : IEventPublisher
    {

        private readonly ConnectionFactory connectionFactory;

        public EventPublisher(IWebHostEnvironment env)
        {
            connectionFactory = new ConnectionFactory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();
            builder.Build().GetSection("RabbitMqSetting").Bind(connectionFactory);

        }

        public void Publish<T>(T @event) where T : DomainEvent
        {
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    var queue = @event.SelectQueue();
                    channel.QueueDeclare(
                        queue: queue,
                        durable:false,
                        exclusive:false,
                        autoDelete:false,
                        arguments:null
                    );
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
                    channel.BasicPublish(
                        exchange:"",
                        routingKey:queue,
                        basicProperties:null,
                        body: body
                    );
                }
            }
        }
    }
}