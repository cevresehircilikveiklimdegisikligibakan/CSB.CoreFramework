using CSB.Core.Utilities.MessageBroking.RabbitMQ.Entities;
using CSB.Core.Utilities.MessageBroking.RabbitMQ.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CSB.Core.Utilities.MessageBroking.RabbitMQ
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddRabbitMQMessageBrokingService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IQueueFactory, QueueFactory>();
            services.AddOptions(configuration);

            return services;
        }

        private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            List<RabbitMQOptions> rabbitMQOptionsList = new List<RabbitMQOptions>();
            RabbitMQOptions[] rabbitMQOptionsArray = configuration.GetSection(nameof(RabbitMQServices)).Get<RabbitMQOptions[]>();
            RabbitMQServices rabbitMQServices = RabbitMQServices.Create(rabbitMQOptionsArray);

            services.Configure<RabbitMQServices>(configureOptions =>
            {
                foreach (var item in rabbitMQServices.RabbitMQOptions)
                {
                    configureOptions.RabbitMQOptions = rabbitMQServices.RabbitMQOptions;
                }
            });

            return services;
        }
    }
}