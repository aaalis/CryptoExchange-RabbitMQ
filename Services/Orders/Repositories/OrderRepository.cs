using System.Net.Mime;
using Orders.Repositories;
using Orders.Models;
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

        public async Task<IEnumerable<Order>> GetOrders(int limit) 
        {
            var orders = await _dbContext.Orders.Where(x => x.Isdeleted == false).Take(limit).ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderById(int id) 
        {
            return await _dbContext.Orders.Where(x => x.Id == id && x.Isdeleted == false).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userid) 
        {
            return await _dbContext.Orders.Where(x=>x.Userid == userid && x.Isdeleted == false).ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Order> UpdateOrder(Order order) 
        {
            var updOrder = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == order.Id && x.Isdeleted == false); 
            if (updOrder == null)
            {
                return null;
            }
            updOrder.ChangeData(order);
            _dbContext.Orders.Update(updOrder);
            await _dbContext.SaveChangesAsync();
            return updOrder;
        }
        public async Task<Order> DeleteOrder(int id) 
        {
            var order = await _dbContext.Orders.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(order != null) 
            {
                order.Isdeleted = true;
                _dbContext.Update(order);
                await _dbContext.SaveChangesAsync();
            }
            return order;
        }

        public async Task<int> GetOrdersCount() 
        {
            return await _dbContext.Orders.CountAsync(x => x.Isdeleted == false);
        }

        public async Task<IEnumerable<Order>> GetMatch(OrdersFilter filter) 
        {
            var orders = await _dbContext.Orders.Where(x =>(x.Basecurrencyid == filter.FirstCurrencyId & 
                                                            x.Quotecurrencyid == filter.SecondCurrencyId) |
                                                           (x.Quotecurrencyid == filter.FirstCurrencyId &
                                                            x.Basecurrencyid == filter.SecondCurrencyId))
                                                .Take(filter.Limit)
                                                .ToListAsync();
            return orders;
        }
    }
}