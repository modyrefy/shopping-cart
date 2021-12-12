using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Server.Core.Interfaces.Caching;
using Server.Model.Dto.Caching;
using System;
using System.Text;

namespace Server.Infrastructure.Provider.Caching
{
    public class DistributedCacheProvider : IDistributedCacheManager
    {

        private readonly IDistributedCache cache;

        public DistributedCacheProvider(IDistributedCache _cache)
        {
            cache = _cache;
        }
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, CacheOptions cacheOptions = null) where T : class
        {
            T item = null;
            var cachevalue = cache.GetString(cacheKey);
            if (string.IsNullOrEmpty(cachevalue))
            {
                if (getItemCallback != null)
                {
                    cacheOptions ??= new CacheOptions();
                    DistributedCacheEntryOptions DefaultPolicy = new()
                    {
                        AbsoluteExpirationRelativeToNow = cacheOptions.AbsoluteExpirationRelativeToNow,
                        SlidingExpiration = cacheOptions.SlidingExpirationMinutes,
                    };
                    item = getItemCallback();
                    cache.SetString(cacheKey, JsonConvert.SerializeObject(item), DefaultPolicy);
                }
            }
            else
            {
                item = JsonConvert.DeserializeObject<T>(cachevalue);
            }
            return item;
        }
        public T Set<T>(string cacheKey, T request, CacheOptions cacheOptions = null) where T : class
        {
            if (request != null)
            {
                cacheOptions ??= new CacheOptions();
                DistributedCacheEntryOptions DefaultPolicy = new()
                {
                    AbsoluteExpirationRelativeToNow = cacheOptions.AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = cacheOptions.SlidingExpirationMinutes,
                };
                cache.SetString(cacheKey, JsonConvert.SerializeObject(request), DefaultPolicy);
            }
            return request;
        }
        public void ResetCache(string cacheKey)
        {
            if (!string.IsNullOrEmpty(cacheKey))
            {
                cache.Remove(cacheKey);
            }
            else
            {
                //IDictionaryEnumerator enumerator = cache.GetEnumerator();
                // List<string> cacheKeys = cache.Get(kvp ).ToList();
                //foreach (string key in cacheKeys)
                //{
                //    cache.Remove(cacheKey);
                //}
            }
        }

       
    }
}
