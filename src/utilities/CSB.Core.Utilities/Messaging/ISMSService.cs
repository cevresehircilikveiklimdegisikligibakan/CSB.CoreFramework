using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSB.Core.Utilities.Messaging
{
    public interface ISMSService
    {
        Task<SMSResponse> SendAsync(SMSMessage message);

        Task<SMSResponse> SendAsync(IList<SMSMessage> messages);
    }
}