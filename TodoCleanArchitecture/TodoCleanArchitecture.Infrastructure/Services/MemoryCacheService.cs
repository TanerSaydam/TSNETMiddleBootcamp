using Microsoft.Extensions.Caching.Memory;
using TodoCleanArchitecture.Application.Services;

namespace TodoCleanArchitecture.Infrastructure.Services;
internal sealed class MemoryCacheService(
    IMemoryCache memoryCache) : ICacheService
{
    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }

    public void Set<T>(string key, T value)
    {
        memoryCache.Set<T>(key, value, new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(60)
        });
    }

    public void TryGetValue<T>(string key, out T? value)
    {
        memoryCache.TryGetValue(key, out value);
    }
}
