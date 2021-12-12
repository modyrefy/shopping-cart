using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Server.Model.Dto.Caching;
using Server.Model.Interfaces.Caching;
using System;
using System.Text;

namespace Server.Infrastructure.Provider.Caching
{
    public class RedisCacheProvider : ICacheManager
    {
        // private static readonly IDistributedCache cache = DistributedCache.;

        private readonly IDistributedCache cache;

        public RedisCacheProvider(IDistributedCache _cache)
        {
            cache = _cache;
        }
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, CacheOptions cacheOptions = null) where T : class
        {
            if (cache.GetString(cacheKey) is not T item)
            {
                cacheOptions ??= new CacheOptions();
                DistributedCacheEntryOptions DefaultPolicy = new()
                {
                    AbsoluteExpiration = cacheOptions.AbsoluteExpirationMinutes,
                    SlidingExpiration = cacheOptions.SlidingExpirationMinutes,
                };
                item = getItemCallback();
                cache.SetString(cacheKey, JsonConvert.SerializeObject(item), DefaultPolicy);
            }
            return JsonConvert.DeserializeObject<T>(item.ToString());
            // return item;
        }

        public void ResetCache(string cacheKey)
        {
            throw new NotImplementedException();
        }
    }
}
