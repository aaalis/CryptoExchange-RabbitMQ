using Orders.Model;

namespace Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GerOrders(int limit);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByOwnerGuid(string ownerGuid);
        Task CreateOrder(Order order);
        Task UpdateOrder(int id, Order order);
        Task<Order> DeleteOrder(int id);
        Task<int> GetOrdersCount();
        Task<IEnumerable<Order>> GetMatch(OrdersFilter filter);
    }
}