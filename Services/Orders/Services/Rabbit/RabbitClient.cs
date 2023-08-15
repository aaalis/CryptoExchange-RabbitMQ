using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Orders.Models;
using RabbitMQ.Client;

namespace Orders.Rabbit;

public class RabbitClient : IRabbitClient
{
    private string _host = "mbroker";
    private string _createOrderQueue = "newOrder.queue";

    private readonly ILogger<RabbitClient> _logger;

    public RabbitClient(ILogger<RabbitClient> logger)
    {
        _logger = logger;
    }
    
    public void CreateOrder(OrderDto orderDto)
    {
        var factory = new ConnectionFactory() { HostName = _host };
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _createOrderQueue, 
                                     durable: false, 
                                     exclusive: false, 
                                     autoDelete: false, 
                                     arguments: null);
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(orderDto));
                channel.BasicPublish(exchange:string.Empty,
                                     routingKey: _createOrderQueue,
                                     basicProperties: null,
                                     body: body);
                _logger.LogInformation($"Order with:{orderDto.Id} sent to queue");
            }
        }
    }
}