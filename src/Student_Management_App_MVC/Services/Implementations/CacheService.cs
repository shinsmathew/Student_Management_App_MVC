using Microsoft.Extensions.Caching.Distributed;
using Student_Management_App_MVC.Services.Interfaces;
using System.Text.Json;


namespace Student_Management_App_MVC.Services.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T> GetRedisCacheAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (cachedData == null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetRedisCacheAsync<T>(string key, T data, TimeSpan timeToLive)
        {
            var serializedData = JsonSerializer.Serialize(data);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
         
            };
            await _cache.SetStringAsync(key, serializedData, options);
        }

        public async Task RemoveRedisCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
    
}
