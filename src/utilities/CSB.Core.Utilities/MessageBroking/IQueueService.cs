using CSB.Core.Entities.Responses;
using System.Threading.Tasks;

namespace CSB.Core.Utilities.MessageBroking
{
    public interface IQueueService
    {
        Task<ServiceResponse> Publish<TRequest>(TRequest request, Queue queue);
        string Name { get; }
    }
}