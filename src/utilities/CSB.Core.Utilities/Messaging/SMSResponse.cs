using CSB.Core.Entities.Responses;

namespace CSB.Core.Utilities.Messaging
{

    public sealed class SMSResponse : ResponseBase
    {
        public string ApiSmsId { get; private set; }

        private static SMSResponse Create(bool success, string apiSmsId, string message)
        {
            SMSResponse result = new SMSResponse();
            result.IsSuccess = success;
            result.ApiSmsId = apiSmsId;
            result.Message = message;
            return result;
        }
        public static SMSResponse Success(string apiSmsId, string message = "")
        {
            return Create(true, apiSmsId, message);
        }
        public static SMSResponse Fail(string message)
        {
            return Create(false, string.Empty, message);
        }
    }
}