using Microsoft.AspNetCore.Mvc;
using Orders.Model;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderDbContext _dbcontext;

        public OrdersController(ILogger<OrdersController> logger, OrderDbContext dbContext)
        {
            _logger = logger;
            _dbcontext = dbContext;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetOrders() 
        {
            var orders = await _dbcontext.Orders.ToListAsync();
            return Ok(orders);
        }
    }
}