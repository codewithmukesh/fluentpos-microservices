namespace BuildingBlocks.Cache;

public interface ICacheRequest
{
    public string? CacheKey { get; }
}