namespace CSB.Core.Utilities.Logging
{
    public sealed class LogSettings<T> where T : Log
    {
        public string QueueName { get; set; }
        public T LogData { get; set; }
        public string LogQueueSectionName { get; set; }

        public static LogSettings<T> Create(string queueName, T logData, string logQueueSectionName)
        {
            return new LogSettings<T>
            {
                LogData = logData,
                LogQueueSectionName = logQueueSectionName,
                QueueName = queueName
            };
        }
    }
}