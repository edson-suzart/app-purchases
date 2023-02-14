using AppPurchases.Shared.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AppPurchases.Shared.Notifications
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly IConnectionFactory _connectionFactory;

        public MessageBrokerService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void SendMessageToQueue<T>(T anyMessage, string queue)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonConvert.SerializeObject(anyMessage);
            var body = Encoding.UTF8.GetBytes(message); 
            
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "purchases",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
