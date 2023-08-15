using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Orders.Models;
using System.Net;
using Orders.Services;

namespace Orders.Controllers
{
    [ApiController]
    [Route("OrdersAPI/")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders() 
        {
            var orders = await _orderService.GetOrders();
            _logger.LogInformation($"{orders.Count()} orders was received");
            return Ok(orders);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<object>> GetOrderById(int id) 
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                _logger.LogError($"Order with id:{id} not found");
                return NotFound($"Order with id:{id} not found");      
            }
            _logger.LogInformation($"Order with id:{id} was received");
            return Ok(order);
        }

        [HttpGet("[action]/{userid}")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userid) 
        {
            var orders = await _orderService.GetOrdersByUserId(userid);
            if (!orders.Any())
            {
                _logger.LogInformation($"Order with userid:{userid} not found");
                return NotFound($"Order with userid:{userid} not found");
            }
            _logger.LogInformation($"Orders with UserId:{userid} was received");
            return Ok(orders);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDto orderDto)
        {
            string message = String.Empty;
            if (ValidationNewOrder(orderDto, out message))
            {
                _logger.LogInformation($"Validation error: {message}");
                return ValidationProblem($"{message}");
            }
            
            await _orderService.CreateOrder(orderDto);

            return CreatedAtAction(nameof(GetOrderById), new {id = orderDto.Id}, orderDto);
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
            
            var orders = await _orderService.GetMatchingOrders(filter);
            
            if (!orders.Any())
            {
                _logger.LogInformation($"Matching orders not found with filter:{filter.ToString()}");
                return NotFound($"Matching orders not found with filter:{filter.ToString()}");
            }
            _logger.LogInformation($"{orders.Count()} was received");
            return Ok(orders);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> UpdateOrder([FromBody] Order order, int id) 
        {
            string message = String.Empty;
            if (ValidationNewOrder(order, out message))
            {
                _logger.LogInformation($"Validation error: {message}");
                return ValidationProblem($"{message}");
            }
            
            await _orderService.UpdateOrder(order, id);

            return AcceptedAtAction(nameof(GetOrderById), new {id}, order);
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> DeleteOrder(int id) 
        {
            var order = await _orderService.DeleteOrder(id);
            if (order == null)
            {
                _logger.LogInformation($"Not found order with id:{id}");
                return NotFound($"Not found order with id:{id}");
            }
            _logger.LogInformation($"Order with id:{id} was deleted");
            return Ok(order);
        }
            
        /// <summary>
        /// Проверка нового ордена
        /// </summary>
        /// <param name="orderDto"></param>
        /// <param name="message"></param>
        /// <returns>true - есть ошибки валидации, false - нет ошибок валидации</returns>
        private bool ValidationNewOrder(Order orderDto, out string message)
        { 
            if (orderDto.Basecurrencyid == orderDto.Quotecurrencyid)
            {
                message = "Basecurrencyid must not be equal to quotocurrencyid";
                return true;
            } 

            message = String.Empty;
            return false;
        }
    }
}