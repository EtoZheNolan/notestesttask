using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Settings;

namespace Notes.Application.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IJsonSerializer _jsonSerializer;

    public CacheService(IDistributedCache distributedCache, IJsonSerializer jsonSerializer) 
    {
        _distributedCache = distributedCache;
        _jsonSerializer = jsonSerializer;
    }
    
    public T? Get<T>(string key)
    {
        var value = _distributedCache.GetString(key);
        
        return value != null ? _jsonSerializer.Deserialize<T>(value) : default;
    }
    
    public void Set<T>(string key, T value, TimeSpan expirationTime)
    {
        var serializedValue = _jsonSerializer.Serialize(value!);
        
        _distributedCache.SetString(key, serializedValue, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime
        });
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        var cachedValue = Get<T>(key);
        if (cachedValue != null)
        {
            value = cachedValue;
            return true;
        }
        
        value = default;
        return false;
    }

    public void Remove(string key)
    {
        _distributedCache.Remove(key);
    }
}