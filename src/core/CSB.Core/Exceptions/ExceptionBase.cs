using System;

namespace CSB.Core.Exceptions
{
    public abstract class ExceptionBase : ApplicationException
    {
        private readonly string _responseCode = "101";

        public ExceptionBase(string responseCode)
        {
            _responseCode = responseCode;
        }
        public ExceptionBase(string responseCode, string message) : base(message)
        {
            _responseCode = responseCode;
        }
        public ExceptionBase(string responseCode, string message, Exception exception) : base(message)
        {
            _responseCode = responseCode;
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}