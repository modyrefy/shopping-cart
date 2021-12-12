using Server.Model.Dto.Caching;
using Server.Model.Interfaces.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Server.Infrastructure.Provider.Caching
{
    public class MemoryCacheProvider : ICacheManager
    {

        private static readonly MemoryCache cache = MemoryCache.Default;
        //private static CacheItemPolicy _defaultPolicy;
        //private static CacheItemPolicy DefaultPolicy
        //{
        //    get
        //    {
        //        return _defaultPolicy ??= new CacheItemPolicy
        //        {
        //            AbsoluteExpiration = DateTimeOffset.MaxValue
        //        };
        //    }
        //    set { _defaultPolicy = value; }
        //}
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, CacheOptions cacheOptions=null) where T : class
        {
            if (cache.Get(cacheKey) is not T item)
            {
                cacheOptions ??= new CacheOptions();
                CacheItemPolicy DefaultPolicy = new()
                {
                    AbsoluteExpiration = cacheOptions.AbsoluteExpirationMinutes,
                    SlidingExpiration = cacheOptions.SlidingExpirationMinutes,
                };
                item = getItemCallback();
                cache.Add(cacheKey, item, DefaultPolicy);
            }
            return item;
        }

        public void ResetCache(string cacheKey)
        {
            if (!string.IsNullOrEmpty(cacheKey))
            {
                cache.Remove(cacheKey);
            }
            else
            {
                List<string> cacheKeys = cache.Select(kvp => kvp.Key).ToList();
                foreach (string key in cacheKeys)
                {
                    cache.Remove(cacheKey);
                }
            }
        }
    }
    
}
