namespace CSB.Core.Utilities.MessageBroking
{
    public interface IQueueFactory
    {
        IQueueService GetService(string serviceName);
    }
}