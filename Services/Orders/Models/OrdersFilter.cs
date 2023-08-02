namespace Orders.Models
{
    public class OrdersFilter
    {
        public int FirstCurrencyId { get; set; }
        public int SecondCurrencyId { get; set; }
        public int Limit { get; set; }

        public OrdersFilter() { }

        public OrdersFilter(int firstCurrency, int secondCurrency, int limit)
        {
            FirstCurrencyId = firstCurrency;
            SecondCurrencyId = secondCurrency;
            Limit = limit;
        }
    }
}