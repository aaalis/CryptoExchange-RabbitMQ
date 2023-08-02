using Orders.Models;
using Orders.Rabbit;
using Orders.Repositories;

namespace Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IRabbitClient _rabbitClient;

    public OrderService(IOrderRepository repository, IRabbitClient rabbitClient)
    {
        _repository = repository;
        _rabbitClient = rabbitClient;
    }
    
    public async Task<IEnumerable<Order>> GetOrders(int limits)
    {
        return await _repository.GetOrders(limits);
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _repository.GetOrderById(id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserId(int userid)
    {
        return await _repository.GetOrdersByUserId(userid);
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

    public async Task<Order> UpdateOrder(Order order)
    {
        return await _repository.UpdateOrder(order);
    }

    public async Task<Order> DeleteOrder(int id)
    {
        return await _repository.DeleteOrder(id);
    }
}