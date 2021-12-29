using CSB.Core.Utilities.Logging;

namespace CSB.Core.Web.Entities.ServiceLogging
{
    public class ServiceLog : Log
    {
        public string Host { get; set; }
        public string RequestIP { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }

        public static ServiceLog Create(string host, string requestIP, string requestData, string responseData)
        {
            return new ServiceLog
            {
                Host = host,
                RequestData = requestData,
                RequestIP = requestIP,
                ResponseData = responseData
            };
        }
    }
}