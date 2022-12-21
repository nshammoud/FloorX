using KQF.Floor.NavServices;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices
{
    public class ItemNumberClient
    {
        private readonly IMemoryCache _cache;
        private readonly int _cacheExpiresInMinutes;

        private const string LIST = "ItemNumberClient.Cache.List";
        public ItemNumberClient(IMemoryCache cache, Microsoft.Extensions.Options.IOptions<NavServiceConfig> config)
        {
            _cache = cache;
            _cacheExpiresInMinutes = config.Value.CacheExpiration ?? 60;
        }

        public IEnumerable<Models.ItemNumber> Get()
        {
            string cacheEntry;

            // Look for cache key.
            if (!_cache.TryGetValue(LIST, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = JsonConvert.SerializeObject(new Models.ItemNumber[] { new Models.ItemNumber() { Name = "asdf" }, new Models.ItemNumber() { Name = "1234" }, new Models.ItemNumber() { Name = "asdf1234" } });

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(_cacheExpiresInMinutes));

                // Save data in cache.
                _cache.Set(LIST, cacheEntry, cacheEntryOptions);
            }

            if (!string.IsNullOrEmpty(cacheEntry))
            {
                return JsonConvert.DeserializeObject<Models.ItemNumber[]>(cacheEntry);
            }
            else return null;

        }
    }
}
