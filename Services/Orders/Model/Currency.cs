using NpgsqlTypes;

namespace Orders.Model
{
    public enum Currency
    {
        [PgName("BTC")]
        BTC,
        [PgName("ETH")]
        ETH,
        [PgName("DASH")]
        DASH
    }
}