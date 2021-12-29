using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Infrastructure.Extensions;
using CSB.Core.Infrastructure.Interceptors.Caching;

namespace CSB.Core.Infrastructure.Interceptors
{
    internal class CachingInterceptor : IInterceptor
    {
        #region [ Variables ]

        private static int ProcessOrder = 1; //Interceptor sıralaması için eklendi. Reflection ile kontrol ediliyor.

        private readonly ICachedInterceptorService _cachedInterceptorService;
        private readonly IClearCacheInterceptorService _clearCacheInterceptorService;

        #endregion

        #region [ Constructors ]

        public CachingInterceptor(ICachedInterceptorService cachedInterceptorService, IClearCacheInterceptorService clearCacheInterceptorService)
        {
            _cachedInterceptorService = cachedInterceptorService;
            _clearCacheInterceptorService = clearCacheInterceptorService;
        }

        #endregion

        public void Intercept(IInvocation invocation)
        {
            ICachingInterceptorService cachingInterceptorService;

            if (invocation.ShouldIntercept<CachedAttribute>())
            {
                cachingInterceptorService = _cachedInterceptorService;
            }
            else if (invocation.ShouldIntercept<ClearCacheAttribute>())
            {
                cachingInterceptorService = _clearCacheInterceptorService;
            }
            else
            {
                invocation.Proceed();
                return;
            }
            cachingInterceptorService.Perform(invocation);
        }
    }
}