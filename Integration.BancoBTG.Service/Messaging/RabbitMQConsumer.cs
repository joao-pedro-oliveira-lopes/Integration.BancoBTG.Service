using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Integration.BancoBTG.Service.Data;
using Integration.BancoBTG.Service.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Integration.BancoBTG.Service.Messaging
{
    public class RabbitMQConsumer
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IModel _channel;
        private readonly string _queueName;
        private readonly RabbitMQConfig _config;

        public RabbitMQConsumer(IConfiguration configuration, IDbContextFactory<ApplicationDbContext> contextFactory, IOptions<RabbitMQConfig> options)
        {
            _contextFactory = contextFactory;
            _config = options.Value;

            var factory = new ConnectionFactory()
            {
                HostName = _config.HostName,
                Port = _config.Port,
                UserName = _config.UserName,
                Password = _config.Password
            };

            _queueName = _config.QueueName;
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var pedido = JsonSerializer.Deserialize<Pedido>(message);

                if (pedido != null)
                {
                    try
                    {
                        using (var context = _contextFactory.CreateDbContext())
                        {
                            context.Pedidos.Add(pedido);
                            await context.SaveChangesAsync();

                            Console.WriteLine($"Pedido {pedido.CodigoPedido} recebido e salvo.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao salvar o pedido: {ex.Message}");
                    }
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }
    }
}
