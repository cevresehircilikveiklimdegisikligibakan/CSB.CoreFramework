using Castle.DynamicProxy;

namespace CSB.Core.Infrastructure.Interceptors.Caching
{
    internal interface ICachingInterceptorService
    {
        void Perform(IInvocation invocation);
    }
}