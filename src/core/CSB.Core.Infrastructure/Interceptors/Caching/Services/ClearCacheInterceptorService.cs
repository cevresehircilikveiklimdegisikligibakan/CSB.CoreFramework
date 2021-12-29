using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Services;
using CSB.Core.Utilities.Caching;
using Microsoft.Extensions.Options;
using System;

namespace CSB.Core.Infrastructure.Interceptors.Caching.Services
{
    internal sealed class ClearCacheInterceptorService : CachingInterceptorServiceBase<ClearCacheAttribute>, IClearCacheInterceptorService
    {
        public ClearCacheInterceptorService(ICacheService cacheService, ISerializer serializer, IOptions<CacheDownOptions> cacheDownOptions) : base(cacheService, serializer, cacheDownOptions)
        {

        }

        private protected override void PerformSync(IInvocation invocation)
        {
            invocation.Proceed();
            _cacheService.Delete(AttributeValue.CacheKey);
        }

        private protected override void PerformAsync(IInvocation invocation)
        {
            throw new NotImplementedException("İhtiyaç olduğunda hazırlanacak");
        }
    }
}