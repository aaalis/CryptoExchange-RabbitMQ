namespace labGRPCa.Model
{
    public class RateFilter
    {
        public OrdersCurrency? Currency { get; set; }
        public DateTime DateFrom { get; set; } 
        public DateTime DateTo { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public List<ActionType> Actions { get; set; }
    }
}
