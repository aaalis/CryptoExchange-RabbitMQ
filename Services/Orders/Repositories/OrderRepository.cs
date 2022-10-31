using System.Net.Mime;
using Orders.Repositories;
using Orders.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Orders.Repositories
{
    public class OrderRepository
    {
        private readonly OrderDbContext dbContext;
        
        public OrderRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Order> GerOrders() 
        {
            return dbContext.Orders.AsEnumerable<Order>();
        }
    }
}