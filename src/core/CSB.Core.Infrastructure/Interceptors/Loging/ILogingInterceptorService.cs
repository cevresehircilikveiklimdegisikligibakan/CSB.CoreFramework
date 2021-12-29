using Castle.DynamicProxy;

namespace CSB.Core.Infrastructure.Interceptors.Loging
{
    internal interface ILogingInterceptorService
    {
        void Perform(IInvocation invocation);
    }
}