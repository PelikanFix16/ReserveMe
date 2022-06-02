using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SharedKernel.Application.EventBus;
using SharedKernel.Domain.Event;

namespace SharedKernel.Infrastructure.EventsBus
{
    public class EventPublisher : IEventPublisher
    {

        private readonly ConnectionFactory connectionFactory;

        public EventPublisher(IConfiguration env)
        {
            connectionFactory = new ConnectionFactory(){
                UserName = env["username"],
                Password = env["password"],
                HostName = env["hostname"],
                Uri = new Uri(env["uri"]),
                VirtualHost = env["virtualhost"]

            };
      
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