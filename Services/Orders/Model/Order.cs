using System.Globalization;
using System.Text.Json.Serialization;

namespace Orders.Model
{
    public class Order
    {   
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public string OwnerGuid { get; set; }

        [JsonIgnore]
        public OrderKind Kind { get; set; }
        [JsonPropertyName("Kind")]
        public string KindString {get => Kind.ToString(); }
        
        public int Count { get; set; }
        
        public decimal Price { get; set; }
        
        [JsonIgnore]
        public Currency BaseCurrency { get; set; }
        [JsonPropertyName("BaseCurrency")]
        public string BaseCurrencyString { get => BaseCurrency.ToString(); }
        
        [JsonIgnore]
        public Currency QuoteCurrency { get; set; }
        [JsonPropertyName("QuoteCurrency")]
        public string QuoteCurrencyString { get => QuoteCurrency.ToString(); }

        public Order()
        {
            
        }

        public Order(int order_id, 
                     string order_ownerguid, 
                     OrderKind order_kind,
                     int order_count, 
                     decimal order_price, 
                     Currency order_baseCurrency, 
                     Currency order_quoteCurrency)
        {
            this.Id = order_id;
            this.OwnerGuid = order_ownerguid;
            this.Kind = order_kind;
            this.Count = order_count;
            this.Price = order_price;
            this.BaseCurrency = order_baseCurrency;
            this.QuoteCurrency = order_quoteCurrency;
        }
    }
}