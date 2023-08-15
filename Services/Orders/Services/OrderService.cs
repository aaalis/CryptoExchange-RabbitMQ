using System.Text;
using Orders.Models;
using Orders.Rabbit;
using Orders.Repositories;
using Orders.Services.Cache;

namespace Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IRabbitClient _rabbitClient;
    private readonly ICacheService _cacheService;

    public OrderService(IOrderRepository repository, IRabbitClient rabbitClient, ICacheService cacheService)
    {
        _repository = repository;
        _rabbitClient = rabbitClient;
        _cacheService = cacheService;
    }
    
    public async Task<IEnumerable<Order>> GetOrders()
    {
        var key = "getOrders";

        var cache = GetCache<IEnumerable<Order>>(key);
        if (cache != null)
        {
            return cache;
        }

        var orders = await _repository.GetOrders();

        AddCache(key, orders);
        
        return orders;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var key = "getOrderById" + id;

        var cacheData = GetCache<Order>(key);
        if (cacheData != null)
        {
            return cacheData;
        }
        var order = await _repository.GetOrderById(id);

        AddCache(key, order);

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserId(int userid)
    {
        var key = "getOrdersByUserId" + userid;

        var cache = GetCache<IEnumerable<Order>>(key);
        if (cache != null)
        {
            return cache;
        }
        var orders = await _repository.GetOrdersByUserId(userid);

        AddCache(key, orders);

        return orders;
    }

    public async Task CreateOrder(OrderDto orderDto)
    {
        await _repository.CreateOrder(new Order(orderDto));
        _rabbitClient.CreateOrder(orderDto);
    }

    public async Task<IEnumerable<Order>> GetMatchingOrders(OrdersFilter filter)
    {
        return await _repository.GetMatch(filter);
    }

    public async Task<Order> UpdateOrder(Order order, int id)
    {
        var updOrder = await _repository.UpdateOrder(order, id);

        _cacheService.RemoveData("getOrderById" + id);

        return updOrder;
    }

    public async Task<Order> DeleteOrder(int id)
    {
        var order = await _repository.DeleteOrder(id);

        _cacheService.RemoveData("getOrderById" + id);

        return order;
    }

    private T? GetCache<T>(string key)
    {
        var cacheData = _cacheService.GetData<T>(key);
        if (cacheData != null)
        {
            return cacheData;
        }

        return default;
    }

    private bool AddCache<T>(string key, T value, int liveTimeInSeconds = 30)
    {
        var expTime = DateTimeOffset.Now.AddSeconds(liveTimeInSeconds);
        return _cacheService.SetData(key, value, expTime);
    }
}