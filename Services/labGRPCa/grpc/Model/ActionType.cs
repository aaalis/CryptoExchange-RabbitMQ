using NpgsqlTypes;
using System.Text.Json.Serialization;

namespace labGRPCa.Model
{
    [PgName("actiontype")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActionType
    {
        [PgName("UP")]
        UP = 0,
        [PgName("DOWN")]
        DOWN = 1
    }
}
