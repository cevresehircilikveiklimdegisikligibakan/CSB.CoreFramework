using CSB.Core.Entities.Responses;

namespace CSB.Core.Services
{
    public interface ISecurityService
    {
        ServiceResponse<string> ConvertToMd5(string text);
    }
}