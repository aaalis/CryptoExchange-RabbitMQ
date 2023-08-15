using System.Text;
using RabbitMQ.Client;

namespace Users.Services.Rabbit
{
    public class RabbitClient : IClient
    {
        public string Host { get; } = "mbroker";

        public string Username { get; } = "user1";

        public string Pass { get; } = "user1";

        public RabbitClient() { }

        public void CreatePortfolio(int id) 
        {
            var factory = new ConnectionFactory() { HostName = Host};
            using (var connection = factory.CreateConnection()) 
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "createPortfolio",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                    var body = Encoding.UTF8.GetBytes(id.ToString());
                    
                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "createPortfolio",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"UserId:${id} sent to create portfolio");
                }
            };
        }

        public void DeletePortfolio(int id) 
        {
            var factory = new ConnectionFactory() {HostName = Host};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "deletePortfolio",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                    var body = Encoding.UTF8.GetBytes(id.ToString());
                    
                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "deletePortfolio",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"UserId:${id} sent to delete portfolio");
                }
            }
        }
    }
}