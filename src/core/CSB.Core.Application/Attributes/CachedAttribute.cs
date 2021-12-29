using System;

namespace CSB.Core.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CachedAttribute : Attribute
    {
        public string CacheKey { get; private set; }
        public int ExpiredInSeconds { get; private set; }

        public CachedAttribute(string cacheKey, int expiredInSeconds = (5 * 60))
        {
            Check(cacheKey, expiredInSeconds);

            CacheKey = cacheKey;
            ExpiredInSeconds = expiredInSeconds;
        }

        private void Check(string cacheKey, int expiredInSeconds)
        {
            Helpers.Check.NotNullOrWhiteSpace(cacheKey, "CacheKey");
            Helpers.Check.ForLessEqualZero(expiredInSeconds, "ExpiredInSeconds");
        }
    }
}