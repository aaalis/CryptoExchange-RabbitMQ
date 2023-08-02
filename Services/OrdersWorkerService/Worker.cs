using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using OrdersWorkerService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrdersWorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string _host = "mbroker"; // container_name
    private readonly string _queueName = "newOrder.queue";
    private IConnection _connection;
    private IModel _channel;
    private string _consumerTag;
    
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var conFactory = new ConnectionFactory() { HostName = _host };

        _connection = conFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _logger.LogInformation("Waiting for Messages...");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, deliverEventArgs) =>
        {
            var body = deliverEventArgs.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            _logger.LogInformation($"Received order: {message}");
            
            var order = JsonSerializer.Deserialize<OrderDto>(message);
            ExecOrder(order);
            _logger.LogInformation($"Exec order: {message}");
        };
        
        _consumerTag = _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.BasicCancel(_consumerTag);
        _channel.Close();
        _connection.Close();
        return base.StopAsync(cancellationToken);
    }

    private void ExecOrder(OrderDto orderDto)
    {
        switch (orderDto.Kind)
        {
            case OrderKind.Buy:
                ExecPurchaseOrder(orderDto);
                break;
            case OrderKind.Sell:
                ExecSellingOrder(orderDto);
                break;
            default:
                _logger.LogInformation("Unknown order kind");
                break;
        }
    }

    private void ExecPurchaseOrder(OrderDto orderDto)
    {
        _channel.BasicPublish(exchange: string.Empty,
                              routingKey: "addAsset.queue",
                              basicProperties: null,
                              body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Asset(orderDto))));
    }

    private void ExecSellingOrder(OrderDto orderDto)
    {
        _channel.BasicPublish(exchange: string.Empty,
                              routingKey: "removeAsset.queue",
                              basicProperties: null,
                              body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Asset(orderDto))));
    }
}