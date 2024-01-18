using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Options;
using Integration.BancoBTG.Service.Models;

namespace Integration.BancoBTG.Service.Messaging
{
    public class RabbitMQProducer
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMQProducer(IOptions<RabbitMQConfig> options)
        {
            var rabbitMQConfig = options.Value;
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                Port = rabbitMQConfig.Port,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password
            };

            _queueName = rabbitMQConfig.QueueName;
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }
    }
}
