using CSB.Core.Entities.Responses;
using CSB.Core.Utilities.Logging.Entities;
using CSB.Core.Utilities.MessageBroking;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CSB.Core.Utilities.Logging.Services
{
    internal sealed class LogService : ILogService
    {
        private readonly IQueueFactory _queueFactory;
        private readonly IOptions<LogOptions> _logOptions;
        public LogService(IQueueFactory queueFactory, IOptions<LogOptions> logOptions)
        {
            _queueFactory = queueFactory;
            _logOptions = logOptions;
        }

        public async Task<ServiceResponse> LogAsync<T>(LogSettings<T> data) where T : Log, new()
        {
            if (_logOptions.Value == null) throw new Exception("LogOptions bulunamadı!");
            var logService = _queueFactory.GetService(data.LogQueueSectionName);
            if (logService == null) throw new Exception(data.LogQueueSectionName + " türünde QueueOption bulunamadı!");

            data.LogData.ApplicationName = _logOptions.Value.ApplicationName;
            var result = await logService.Publish(data.LogData, Queue.Create(data.QueueName));
            return result;
        }
        public ServiceResponse Log<T>(LogSettings<T> data) where T : Log, new()
        {
            if (_logOptions.Value == null) throw new Exception("LogOptions bulunamadı!");
            var logService = _queueFactory.GetService(data.LogQueueSectionName);
            if (logService == null) throw new Exception(data.LogQueueSectionName + " türünde QueueOption bulunamadı!");

            data.LogData.ApplicationName = _logOptions.Value.ApplicationName;
            var result = logService.Publish(data.LogData, Queue.Create(data.QueueName));
            result.Wait();
            return result.Result;
        }
    }
}