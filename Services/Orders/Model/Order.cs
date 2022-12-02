using System.Globalization;
using System.Text.Json.Serialization;

namespace Orders.Model
{
    public class Order
    {   
        //[JsonIgnore]
        public int Id { get; set; }

        public string OwnerGuid { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderKind Kind { get; set; }
        
        public int Count { get; set; }
        
        public decimal Price { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]           
        public OrdersCurrency BaseCurrency { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrdersCurrency QuoteCurrency { get; set; }

        public Order()
        {
            
        }

        public Order(int id, 
                     string ownerguid, 
                     OrderKind kind,
                     int count, 
                     decimal price, 
                     OrdersCurrency basecurrency, 
                     OrdersCurrency quotecurrency)
        {
            this.Id = id;
            this.OwnerGuid = ownerguid;
            this.Kind = kind;
            this.Count = count;
            this.Price = price;
            this.BaseCurrency = basecurrency;
            this.QuoteCurrency = quotecurrency;
        }

        public Order(string ownerguid, 
                     OrderKind kind,
                     int count, 
                     decimal price, 
                     OrdersCurrency basecurrency, 
                     OrdersCurrency quotecurrency)
        {
            this.OwnerGuid = ownerguid;
            this.Kind = kind;
            this.Count = count;
            this.Price = price;
            this.BaseCurrency = basecurrency;
            this.QuoteCurrency = quotecurrency;
        }
    }
}