using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Services;

namespace CSB.Core.Infrastructure.Interceptors.Loging.Services
{
    internal sealed class LogInterceptorService : LogingInterceptorServiceBase<LogAttribute>, ILogInterceptorService
    {
        private readonly ISerializer _serializer;

        public LogInterceptorService(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Perform(IInvocation invocation)
        {
            string methodName = invocation.Method.Name;
            var parameters = _serializer.Serialize(invocation.Arguments);
            invocation.Proceed();
            var returnValue = _serializer.Serialize(invocation.ReturnValue);

            //TODO: ElasticSearch'e logla.
            //Parameters, MethodName ve ReturnValue değerlerini tek class altında toplayıp kaydet.

        }
    }
}