using CSB.Core.Entities.Responses;
using System.Collections.Generic;

namespace CSB.Core.Utilities.Messaging
{
    public interface IEmailService
    {
        ServiceResponse Send(EMailMessage message);
        ServiceResponse Send(IList<EMailMessage> messages);
    }
}