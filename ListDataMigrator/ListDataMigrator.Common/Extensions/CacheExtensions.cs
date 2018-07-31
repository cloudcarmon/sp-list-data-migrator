using System;
using System.Runtime.Caching;

namespace ListDataMigrator.Common.Extensions
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ObjectCache cache, string key)
        {
            var cachedItem = cache.Get(key);
            if (cachedItem is T)
            {
                return (T)cachedItem;
            }
            return default(T);
        }

        private static T Invoke<T>(Delegate callback, params object[] arguments)
        {
            var result = callback.DynamicInvoke(arguments);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static T Ensure<T>(this ObjectCache cache, string key, params object[] arguments)
        {
            var cachedItem = cache.Get<T>(key);
            if (cachedItem == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                var assemblyName = typeof(T).ToString();
                var delegateType = cache.Get<Delegate>(assemblyName);
                if (delegateType != null)
                {
                    var ensured = Invoke<T>(delegateType, arguments);
                    cache.Set(key, ensured, policy);
                    cachedItem = ensured;
                }
            }
            return cachedItem;
        }

        public static void RegisterTypeDelegate<T>(this ObjectCache cache, Delegate callback)
        {
            var assemblyName = typeof(T).ToString();
            var cachedItem = cache.Get<T>(assemblyName);
            if (cachedItem == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                cache.Set(assemblyName, callback, policy);
            }
        }
    }
}
