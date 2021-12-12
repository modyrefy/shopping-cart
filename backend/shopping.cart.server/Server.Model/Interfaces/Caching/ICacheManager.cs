using Server.Model.Dto.Caching;
using System;

namespace Server.Model.Interfaces.Caching
{
    public interface ICacheManager
    {
        // public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, CacheOptions cacheOptions =null) where T : class;
        public void ResetCache(string cacheKey);
    }
   
}
