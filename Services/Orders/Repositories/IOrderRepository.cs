using Orders.Models;

namespace Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userid);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order, int id);
        Task<Order> DeleteOrder(int id);
        Task<int> GetOrdersCount();
        Task<IEnumerable<Order>> GetMatch(OrdersFilter filter);
    }
}