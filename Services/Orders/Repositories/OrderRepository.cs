using System.Net.Mime;
using Orders.Repositories;
using Orders.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        
        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GerOrders(int limit) 
        {
            var orders = await _dbContext.Orders.Take(limit).ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderById(int id) 
        {
            return await _dbContext.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByOwnerGuid(string ownerGuid) 
        {
            return await _dbContext.Orders.Where(x=>x.OwnerGuid == ownerGuid).ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateOrder(int id, Order order) 
        {
            order.Id = id;
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Order> DeleteOrder(int id) 
        {
            var order = await _dbContext.Orders.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(order != null) 
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
            return order;
        }

        public async Task<int> GetOrdersCount() 
        {
            return await _dbContext.Orders.CountAsync();
        }

        public async Task<IEnumerable<Order>> GetMatch(OrdersFilter filter) 
        {
            var orders = await _dbContext.Orders.Where(x =>(x.BaseCurrency == filter.FirstCurrency & 
                                                            x.QuoteCurrency == filter.SecondCurrency) |
                                                           (x.QuoteCurrency == filter.FirstCurrency &
                                                            x.BaseCurrency == filter.SecondCurrency))
                                                .Take(filter.Limit)
                                                .ToListAsync();
            return orders;
        }
    }
}