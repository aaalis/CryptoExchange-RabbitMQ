using Orders.Model;

namespace Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GerOrders();
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByOwnerGuid(string ownerGuid);
        Task CreateOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}