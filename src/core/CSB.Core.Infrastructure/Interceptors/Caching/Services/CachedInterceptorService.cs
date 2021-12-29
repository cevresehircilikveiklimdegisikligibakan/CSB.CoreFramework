using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Entities.Responses;
using CSB.Core.Extensions;
using CSB.Core.Services;
using CSB.Core.Utilities.Caching;
using Microsoft.Extensions.Options;
using System;

namespace CSB.Core.Infrastructure.Interceptors.Caching.Services
{
    internal sealed class CachedInterceptorService : CachingInterceptorServiceBase<CachedAttribute>, ICachedInterceptorService
    {
        public CachedInterceptorService(ICacheService cacheService, ISerializer serializer, IOptions<CacheDownOptions> cacheDownOptions) : base(cacheService, serializer, cacheDownOptions)
        {

        }

        private protected override void PerformSync(IInvocation invocation)
        {
            string cacheKey = AttributeValue.CacheKey;
            var returnCacheValue = GetCacheValue(cacheKey);
            bool cacheIsAlive = returnCacheValue.Item1;
            string returnValue = returnCacheValue.Item2;

            if (returnValue != null)
            {
                Type returnType = invocation.Method.ReturnType;

                if (returnType.IsGeneric() && returnType.Name == typeof(ServiceResponse<>).Name)
                {
                    var subType = returnType.GetGenericArguments()[0];
                    var value = _serializer.Deserialize(returnValue, subType);
                    invocation.ReturnValue = Activator.CreateInstance(returnType, new object[] { true, value });
                }
                else
                {
                    var value = _serializer.Deserialize(returnValue, returnType);
                    invocation.ReturnValue = value;
                }

            }
            else
            {
                invocation.Proceed();
                if (cacheIsAlive)
                {
                    object cachedValue = null;
                    if (invocation.ReturnValue.GetType().IsGeneric())
                    {
                        if (invocation.ReturnValue.GetType().GenericTypeArguments[0].IsResponse())
                        {
                            var value = GetResponseValue(invocation);
                            if (value != null)
                            {
                                bool isGenericList = invocation.ReturnValue.GetType().GenericTypeArguments[0].IsGenericList();
                                if ((isGenericList && value.Count > 0) || !isGenericList)
                                    cachedValue = GetValue(invocation);
                            }
                        }
                        else if (invocation.ReturnValue.GetType().GenericTypeArguments[0].IsGenericList())
                        {
                            var value = GetValue(invocation);
                            if (value != null && value.Count > 0)
                                cachedValue = value;
                        }
                        else
                        {
                            var value = GetValue(invocation);
                            if (value != null)
                                cachedValue = value;
                        }
                    }
                    else
                    {
                        var value = invocation.ReturnValue;
                        if (value != null)
                            cachedValue = value;
                    }

                    if (cachedValue != null)
                    {
                        var expiredSeconds = AttributeValue.ExpiredInSeconds;
                        _cacheService.Set(cacheKey, cachedValue, TimeSpan.FromSeconds(expiredSeconds));
                    }
                }
            }
        }

        private protected override void PerformAsync(IInvocation invocation)
        {
            string cacheKey = AttributeValue.CacheKey;
            var returnCacheValue = GetCacheValue(cacheKey);
            bool cacheIsAlive = returnCacheValue.Item1;
            string returnValue = returnCacheValue.Item2;

            if (returnValue != null)
            {
                Type returnType = invocation.Method.ReturnType;
                if (returnType.IsGenericType)
                    returnType = returnType.GenericTypeArguments[0];

                //TODO: Async methodların Task<T> dönüşü yapılacak.
                if (returnType.IsGeneric() && returnType.Name == typeof(ServiceResponse<>).Name)
                {
                    var subType = returnType.GetGenericArguments()[0];
                    var value = _serializer.Deserialize(returnValue, subType);
                    invocation.ReturnValue = Activator.CreateInstance(returnType, new object[] { true, value });
                }
                else
                {
                    var value = _serializer.Deserialize(returnValue, returnType);
                    invocation.ReturnValue = value;
                }

            }
            else
            {
                invocation.Proceed();
                if (cacheIsAlive)
                {
                    var returnValueResult = ((dynamic)invocation.ReturnValue).Result;
                    var returnValueResultType = ((object)returnValueResult).GetType();
                    object cachedValue = null;
                    if (returnValueResultType.IsGeneric())
                    {
                        if (returnValueResultType.GenericTypeArguments[0].IsResponse())
                        {
                            var value = GetResponseValue(invocation);
                            if (value != null)
                            {
                                bool isGenericList = returnValueResultType.GenericTypeArguments[0].IsGenericList();
                                if ((isGenericList && value.Count > 0) || !isGenericList)
                                    cachedValue = GetValue(invocation);
                            }
                        }
                        else if (returnValueResultType.GenericTypeArguments[0].IsGenericList())
                        {
                            var value = GetValue(invocation);
                            if (value != null && value.Count > 0)
                                cachedValue = value;
                        }
                        else
                        {
                            var value = GetValueAsync(invocation);
                            if (value != null)
                                cachedValue = value;
                        }
                    }
                    else
                    {
                        var value = returnValueResult;
                        if (value != null)
                            cachedValue = value;
                    }

                    if (cachedValue != null)
                    {
                        var expiredSeconds = AttributeValue.ExpiredInSeconds;
                        _cacheService.Set(cacheKey, cachedValue, TimeSpan.FromSeconds(expiredSeconds));
                    }
                }
            }
        }
    }
}