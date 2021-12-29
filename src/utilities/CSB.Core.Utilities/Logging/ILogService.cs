using CSB.Core.Entities.Responses;
using System.Threading.Tasks;

namespace CSB.Core.Utilities.Logging
{
    public interface ILogService
    {
        Task<ServiceResponse> LogAsync<T>(LogSettings<T> data) where T : Log, new();
    }
}