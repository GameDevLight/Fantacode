using AspNetCoreRateLimit;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class RedisRateLimitCounterStore : IRateLimitCounterStore
{
    private readonly IDistributedCache _cache;

    public RedisRateLimitCounterStore(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        var data = await _cache.GetAsync(id, cancellationToken);
        return data != null;
    }

    public async Task<RateLimitCounter?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var data = await _cache.GetAsync(id, cancellationToken);
        if (data == null) return null;

        return JsonSerializer.Deserialize<RateLimitCounter>(data);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(id, cancellationToken);
    }

    public async Task SetAsync(string id, RateLimitCounter counter, TimeSpan expirationTime, CancellationToken cancellationToken = default)
    {
        var data = JsonSerializer.SerializeToUtf8Bytes(counter);
        await _cache.SetAsync(id, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime
        }, cancellationToken);
    }

    public async Task ResetAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task SetAsync(string id, RateLimitCounter? entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
    {
        if (entry == null) return;

        var data = JsonSerializer.SerializeToUtf8Bytes(entry);
        await _cache.SetAsync(id, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromMinutes(1)
        }, cancellationToken);
    }

}
