using NpgsqlTypes;
using System.Text.Json.Serialization;

namespace Orders.Model
{
    [PgName("orderscurrency")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrdersCurrency
    {
        [PgName("BTC")]
        BTC = 0,
        [PgName("ETH")]
        ETH = 1,
        [PgName("DASH")]
        DASH =2
    }
}