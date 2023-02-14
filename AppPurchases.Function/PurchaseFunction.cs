using AppPurchases.Function.ContractsServices;
using AppPurchases.Function.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace AppPurchases.Function
{
    public class PurchaseFunction 
    {
        private readonly IPurchaseServiceFunction _purchaseServiceFunction;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public PurchaseFunction(IPurchaseServiceFunction purchaseServiceFunction, IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _purchaseServiceFunction = purchaseServiceFunction;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("purchases", false, false, false, null);
        }

        [FunctionName("purchase-function")]
        public async Task ProcessPurchases([RabbitMQTrigger("purchases", ConnectionStringSetting = "connectionRabbitMq")]string message)
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (sender, eventArgs) =>
                {
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                };

                var messageModel = JsonConvert.DeserializeObject<PurchaseMessageModel>(message);
                await _purchaseServiceFunction.Process(messageModel);

                _channel.BasicConsume("purchases", false, consumer);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
