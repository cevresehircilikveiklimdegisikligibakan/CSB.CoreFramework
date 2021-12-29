using System;

namespace CSB.Core.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ClearCacheAttribute : Attribute
    {
        public string CacheKey { get; private set; }

        public ClearCacheAttribute(string cacheKey)
        {
            Check(cacheKey);

            CacheKey = cacheKey;
        }

        private void Check(string cacheKey)
        {
            Helpers.Check.NotNullOrWhiteSpace(cacheKey, "CacheKey");
        }
    }
}