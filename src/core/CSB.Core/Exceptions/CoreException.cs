using System;

namespace CSB.Core.Exceptions
{
    public sealed class CoreException : ExceptionBase
    {
        private const string SERVICE_RESPONSE_CODE = "201";
        public CoreException() : base(SERVICE_RESPONSE_CODE) { }
        public CoreException(string message) : base(SERVICE_RESPONSE_CODE, $"CoreException: {message}") { }
        public CoreException(string message, Exception exception) : base(SERVICE_RESPONSE_CODE, $"CoreException: {message}", exception) { }
    }
}