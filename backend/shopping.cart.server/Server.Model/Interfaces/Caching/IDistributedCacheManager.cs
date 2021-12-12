using Server.Model.Dto.Caching;
using System;

namespace Server.Core.Interfaces.Caching
{
    public interface IDistributedCacheManager
    {
        // public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, CacheOptions cacheOptions = null) where T : class;
        public T Set<T>(string cacheKey,T request, CacheOptions cacheOptions = null) where T : class;
       // public T Get<T>(string cacheKey) where T : class;
        public void ResetCache(string cacheKey);
    }
}
