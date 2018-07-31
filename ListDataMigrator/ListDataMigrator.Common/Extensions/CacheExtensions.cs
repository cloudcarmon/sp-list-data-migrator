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
    }
}
