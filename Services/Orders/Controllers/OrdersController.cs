using Microsoft.AspNetCore.Mvc;
using Orders.Model;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orders.Repositories;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private IOrderRepository _orderRepository;

        public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }
        
        [HttpGet("[action]/{limit}")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int limit) 
        {
            var orders = await _orderRepository.GerOrders(limit);
            _logger.LogInformation($"{orders.Count()} orders shown");
            return Ok(orders);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<object>> GetOrderById(int id) 
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                _logger.LogError($"Order with id:{id} not found");
                return NotFound($"Order with id:{id} not found");      
            }
            return Ok(order);
        }

        [HttpGet("[action]/{ownerGuid}")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByOwnerGuid(string ownerGuid) 
        {
            var orders = await _orderRepository.GetOrdersByOwnerGuid(ownerGuid);
            if (!orders.Any())
            {
                _logger.LogInformation($"Order with ownerGuid:{ownerGuid} not found");
                return NotFound($"Order with ownerGuid:{ownerGuid} not found");
            }
            return Ok(orders);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order) 
        {
            await _orderRepository.CreateOrder(order);

            return CreatedAtAction(nameof(GetOrderById), new {id = order.Id}, order);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Order>>> GetMatchingOrders([FromQuery] OrdersFilter filter) 
        {
            if (filter.Limit == 0)
            {
                _logger.LogInformation($"ValidationProblem, limit:{filter.Limit}");
                return ValidationProblem($"ValidationProblem, limit:{filter.Limit}");
            }
            
            var orders = await _orderRepository.GetMatch(filter);
            
            if (!orders.Any())
            {
                _logger.LogInformation($"Matching orders not found with filter:{filter.ToString()}");
                return NotFound($"Matching orders not found with filter:{filter.ToString()}");
            }
            return Ok(orders);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order order) 
        {
            await _orderRepository.UpdateOrder(id, order);

            return AcceptedAtAction(nameof(GetOrderById), new {id = order.Id}, order);
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> DeleteOrder(int id) 
        {
            var order = await _orderRepository.DeleteOrder(id);
            if (order == null)
            {
                _logger.LogInformation($"Not found order with id:{id}");
                return NotFound($"Not found order with id:{id}");
            }
            return Ok(order);
        }
    }
}