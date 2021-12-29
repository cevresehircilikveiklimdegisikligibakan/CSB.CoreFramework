using CSB.Core.Utilities.Logging;

namespace CSB.Core.Web.Entities.ExceptionLogging
{
    public class ExceptionLog : Log
    {
        public string Id { get; set; }
        public string Host { get; set; }
        public string RequestIP { get; set; }
        public string RequestData { get; set; }
        public string Exception { get; set; }

        public static ExceptionLog Create(string id, string host, string requestIP, string requestData, string exception)
        {
            return new ExceptionLog
            {
                Id = id,
                Host = host,
                RequestIP = requestIP,
                RequestData = requestData,
                Exception = exception
            };
        }
    }
}