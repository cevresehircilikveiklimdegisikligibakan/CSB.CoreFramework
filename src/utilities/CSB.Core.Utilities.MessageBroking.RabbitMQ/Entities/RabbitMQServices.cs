using System.Collections.Generic;

namespace CSB.Core.Utilities.MessageBroking.RabbitMQ.Entities
{
    internal record RabbitMQServices
    {
        public IList<RabbitMQOptions> RabbitMQOptions { get; set; }

        public static RabbitMQServices Create(RabbitMQOptions[] rabbitMQOptionsArray)
        {
            return new RabbitMQServices
            {
                RabbitMQOptions = rabbitMQOptionsArray
            };
        }
    }
}