using System.Text.Json;
using StackExchange.Redis;
using Order = Orders.Models.Order;

namespace Orders.Services.Cache;

public class CacheService : ICacheService
{
    private IDatabase _cacheDb;
    private IConfiguration _configuration;

    public CacheService(IConfiguration configuration)
    {
        _configuration = configuration;
        
        var redis = ConnectionMultiplexer.Connect(_configuration.GetSection("Redis:Connection").Value);
        _cacheDb = redis.GetDatabase();
    }
    
    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expTime)
    {
        var expirationTime = expTime.DateTime.Subtract(DateTime.Now);
        var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirationTime);
        return isSet;
    }

    public object RemoveData(string key)
    {
        var exist = _cacheDb.KeyExists(key);
        if (exist)
        {
            return _cacheDb.KeyDelete(key);
        }
        return false;    
    }
}