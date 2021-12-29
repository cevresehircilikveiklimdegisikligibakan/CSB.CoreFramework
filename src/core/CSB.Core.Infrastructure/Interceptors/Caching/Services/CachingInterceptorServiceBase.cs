using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Extensions;
using CSB.Core.Infrastructure.Extensions;
using CSB.Core.Services;
using CSB.Core.Utilities.Caching;
using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace CSB.Core.Infrastructure.Interceptors.Caching.Services
{
    internal abstract class CachingInterceptorServiceBase<TAttribute> where TAttribute : Attribute
    {
        #region [ Variables ]

        private protected readonly ICacheService _cacheService;
        private protected readonly ISerializer _serializer;
        private protected readonly IOptions<CacheDownOptions> _cacheDownOptions;

        #endregion

        #region [ Properties ]

        private protected static DateTime? LastDownTime { get; set; }
        private protected static int? WaitTime { get; set; }

        private protected TAttribute AttributeValue { get; private set; }

        #endregion

        #region [ Constructors ]

        public CachingInterceptorServiceBase(ICacheService cacheService, ISerializer serializer, IOptions<CacheDownOptions> cacheDownOptions)
        {
            _cacheService = cacheService;
            _cacheDownOptions = cacheDownOptions;
            _serializer = serializer;
        }

        #endregion

        #region [ Perform ]

        public void Perform(IInvocation invocation)
        {
            var method = invocation.GetMethodInfo();
            AttributeValue = method.GetCustomAttributeOrNull<TAttribute>();
            if (IsCacheDown)
            {
                invocation.Proceed();
                return;
            }

            if (method.IsAsync())
                PerformAsync(invocation);
            else
                PerformSync(invocation);
        }

        private protected abstract void PerformSync(IInvocation invocation);
        private protected abstract void PerformAsync(IInvocation invocation);

        #endregion

        #region [ Helpers #Private ]


        private bool IsCachedAttribute<T>()
        {
            return typeof(T) == typeof(CachedAttribute);
        }
        private bool IsClearCacheAttribute<T>()
        {
            return typeof(T) == typeof(ClearCacheAttribute);
        }
        private protected dynamic GetValue(IInvocation invocation)
        {
            return ((dynamic)invocation.ReturnValue).Data;
        }
        private protected dynamic GetValueAsync(IInvocation invocation)
        {
            return ((dynamic)invocation.ReturnValue).Result.Data;
        }

        private protected dynamic GetResponseValue(IInvocation invocation)
        {
            return ((dynamic)invocation.ReturnValue).Result.Data;
        }
        private protected dynamic GetTaskValue(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [ Cache Down Operations #Private ]

        private protected Tuple<bool, string> GetCacheValue(string cacheKey)
        {
            try
            {
                CheckCacheIsAlive();

                string returnValue = _cacheService.Get(cacheKey);

                ClearCacheIsDownParameters();

                return new Tuple<bool, string>(true, returnValue);
            }
            catch
            {
                SetCacheIsDownParameters();

                return new Tuple<bool, string>(false, null);
            }
        }
        private protected void CheckCacheIsAlive()
        {
            if (LastDownTime.HasValue)
                SetCurrentTime();
        }
        private protected void ClearCacheIsDownParameters()
        {
            if (LastDownTime.HasValue)
            {
                LastDownTime = null;
                WaitTime = null;
            }
        }
        private protected void SetCurrentTime()
        {
            var currentTime = DateTime.UtcNow.ToString();
            byte[] encodedCurrentTime = Encoding.UTF8.GetBytes(currentTime);
            _cacheService.Set("cachedTime", encodedCurrentTime.ToString());
        }
        private protected void SetCacheIsDownParameters()
        {
            if (LastDownTime.HasValue == false)
                LastDownTime = DateTime.Now;
            int oldWaitTime = WaitTime.HasValue ? WaitTime.Value : 0;
            WaitTime = _cacheDownOptions.Value.GetNext(WaitTime);
            if (WaitTime == oldWaitTime) //Son kontrol aşamasındaysa tarihi sıfırla.
                LastDownTime = DateTime.Now;
        }

        private protected bool IsLastDownTimeExpired
        {
            get => _cacheDownOptions.Value.GetWaitTimeShouldBe(LastDownTime.Value) >= WaitTime;
        }
        private protected bool IsCacheDown
        {
            get => LastDownTime.HasValue && !IsLastDownTimeExpired;
        }

        #endregion
    }
}