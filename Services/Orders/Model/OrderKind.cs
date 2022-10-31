using NpgsqlTypes;

namespace Orders.Model
{
    public enum OrderKind
    {
        [PgName("Buy")]
        Buy,
        [PgName("Sell")]
        Sell
    }
}