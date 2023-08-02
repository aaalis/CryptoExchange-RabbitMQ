using Orders.Models;

namespace Orders.Services;

public interface IOrderService
{
    public Task<IEnumerable<Order>> GetOrders(int limits);
    public Task<Order> GetOrderById(int id);
    public Task<IEnumerable<Order>> GetOrdersByUserId(int userid);
    public Task CreateOrder(OrderDto orderDto);
    public Task<IEnumerable<Order>> GetMatchingOrders(OrdersFilter filter);
    public Task<Order> UpdateOrder(Order order);
    public Task<Order> DeleteOrder(int id);
}