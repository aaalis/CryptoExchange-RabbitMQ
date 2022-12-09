using System.Text.Json.Serialization;

namespace gRPC_Server.Model
{
    public class CurrencyRate
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ActionType BackRefAction { get; set; }

        public DateTime DateOfChange { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrdersCurrency Currency { get; set; }

        public decimal Price { get; set; }
    }
}
