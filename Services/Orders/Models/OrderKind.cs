using NpgsqlTypes;
using System.Text.Json.Serialization;

namespace Orders.Models
{
    [PgName("orderskind")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderKind
    {
        [PgName("Buy")]
        Buy = 0,
        [PgName("Sell")]
        Sell = 1
    }
}