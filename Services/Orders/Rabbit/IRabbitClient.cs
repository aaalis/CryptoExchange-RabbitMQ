using Orders.Models;

namespace Orders.Rabbit;

public interface IRabbitClient
{
    public void CreateOrder(OrderDto orderDto);
}