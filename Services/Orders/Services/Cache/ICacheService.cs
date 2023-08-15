using Orders.Models;

namespace Orders.Services.Cache;

public interface ICacheService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T value, DateTimeOffset expTime);
    object RemoveData(string key);
}