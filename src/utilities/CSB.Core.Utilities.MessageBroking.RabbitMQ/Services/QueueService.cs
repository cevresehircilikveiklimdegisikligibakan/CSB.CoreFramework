using CSB.Core.Entities.Responses;
using CSB.Core.Services;
using CSB.Core.Utilities.MessageBroking.RabbitMQ.Entities;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace CSB.Core.Utilities.MessageBroking.RabbitMQ.Services
{
    internal sealed class QueueService : IQueueService
    {
        private readonly RabbitMQOptions _options;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IEncoder _encoder;

        public QueueService(RabbitMQOptions options, IEncoder encoder)
        {
            _encoder = encoder;
            _options = options;

            _connectionFactory = new ConnectionFactory
            {
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                HostName = _options.HostName,
                Port = _options.Port
            };
        }
        public async Task<ServiceResponse> Publish<TRequest>(TRequest request, Queue queue)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue.Name,
                                            durable: queue.Durable,
                                            exclusive: queue.Exclusive,
                                            autoDelete: queue.AutoDelete,
                                            arguments: queue.Arguments);
                    var body = _encoder.GetBytes(request);
                    channel.BasicPublish(exchange: queue.Exchange,
                                            routingKey: queue.Name,
                                            basicProperties: null,
                                            body: body);
                }
            }
            return ServiceResponse.Success("İstek kuyruğa iletildi.");
        }

        public string Name
        {
            get { return _options.Name; }
        }
    }
}