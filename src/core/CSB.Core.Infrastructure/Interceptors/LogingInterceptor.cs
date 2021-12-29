using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Infrastructure.Extensions;
using CSB.Core.Infrastructure.Interceptors.Loging;

namespace CSB.Core.Infrastructure.Interceptors
{
    internal class LogingInterceptor : IInterceptor
    {
        private static int ProcessOrder = 2;

        private readonly ILogInterceptorService _logInterceptorService;

        public LogingInterceptor(ILogInterceptorService logInterceptorService)
        {
            _logInterceptorService = logInterceptorService;
        }

        public void Intercept(IInvocation invocation)
        {
            ILogingInterceptorService logingInterceptorService;

            if (invocation.ShouldIntercept<LogAttribute>())
            {
                logingInterceptorService = _logInterceptorService;
            }
            else
            {
                invocation.Proceed();
                return;
            }
            logingInterceptorService.Perform(invocation);
        }
    }
}