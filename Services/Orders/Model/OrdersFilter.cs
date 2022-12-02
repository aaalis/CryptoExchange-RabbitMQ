namespace Orders.Model
{
    public class OrdersFilter
    {
        public OrdersCurrency FirstCurrency { get; set; }
        public OrdersCurrency SecondCurrency { get; set; }
        public int Limit { get; set; }

        public OrdersFilter() { }

        public OrdersFilter(OrdersCurrency firstCurrency, OrdersCurrency secondCurrency, int limit)
        {
            FirstCurrency = firstCurrency;
            SecondCurrency = SecondCurrency;
            Limit = limit;
        }
    }
}