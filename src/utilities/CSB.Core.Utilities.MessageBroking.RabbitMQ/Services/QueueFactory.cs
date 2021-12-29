using CSB.Core.Exceptions;
using CSB.Core.Services;
using CSB.Core.Utilities.MessageBroking.RabbitMQ.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace CSB.Core.Utilities.MessageBroking.RabbitMQ.Services
{
    internal class QueueFactory : IQueueFactory
    {
        private List<IQueueService> QueueServices { get; set; } = new List<IQueueService>();
        private readonly RabbitMQServices _serviceOptions;
        private readonly IEncoder _encoder;

        public QueueFactory(IOptions<RabbitMQServices> serviceOptions, IEncoder encoder)
        {
            _serviceOptions = serviceOptions.Value;
            _encoder = encoder;
            GenerateQueueServices();
        }

        #region [ GenerateQueueServices ]
        private void GenerateQueueServices()
        {
            CheckQueueServiceNames();
            PopulateQueueServices();
        }
        private void CheckQueueServiceNames()
        {
            bool hasMultipleName = (from x in _serviceOptions.RabbitMQOptions
                                    group x by x.Name into nameGroup
                                    select new
                                    {
                                        Key = nameGroup.Key,
                                        Count = nameGroup.Count()
                                    }).Count(x => x.Count > 1) > 0;
            if (hasMultipleName)
                throw new CoreException("Sistem ayarlarında aynı isimde birden fazla kuyruk tanımı yapılmıştır. Lütfen kuyruk ayarlarını kontrol ediniz.");
        }
        private void PopulateQueueServices()
        {
            if (_serviceOptions.RabbitMQOptions?.Count > 0)
            {
                foreach (var options in _serviceOptions.RabbitMQOptions)
                {
                    if (!QueueServices.Any(x => x.Name == options.Name))
                        QueueServices.Add(new QueueService(options, _encoder));
                }
            }
        }
        #endregion

        public IQueueService GetService(string name)
        {
            return QueueServices.Where(x => x.Name == name).FirstOrDefault();
        }
    }
}