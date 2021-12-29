using CSB.Core.Exceptions;

namespace CSB.Core.Domain.Exceptions
{
    public sealed class ServiceException : ExceptionBase
    {
        private const string SERVICE_RESPONSE_CODE = "201";
        public ServiceException() : base(SERVICE_RESPONSE_CODE) { }
        public ServiceException(string message) : base(SERVICE_RESPONSE_CODE, message) { }
    }
}