using Orders.Models;

namespace Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders(int limit);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userid);
        Task CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> DeleteOrder(int id);
        Task<int> GetOrdersCount();
        Task<IEnumerable<Order>> GetMatch(OrdersFilter filter);
    }
}