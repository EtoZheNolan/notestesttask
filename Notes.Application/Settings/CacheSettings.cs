using Notes.Application.Enums;

namespace Notes.Application.Settings;

public class CacheSettings
{
    public CacheType CacheType { get; init; }
    
    public int ExpirationTimeInMinutes { get; init; }
    
    public RedisSettings RedisSettings { get; init; } = null!;
}

public class RedisSettings
{
    public string ConnectionString { get; init; } = null!;

    public string InstanceName { get; init; } = null!;
}