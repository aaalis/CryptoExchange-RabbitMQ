using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Orders.Models
{
    public class Order
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Userid { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderKind Kind { get; set; }
        public int? Count { get; set; }
        public decimal? Price { get; set; }
        public int? Basecurrencyid { get; set; }
        public int? Quotecurrencyid { get; set; }
        [JsonIgnore]
        public bool Isdeleted { get; set; }

        [JsonIgnore]
        public virtual Currency? Basecurrency { get; set; }
        [JsonIgnore]
        public virtual Currency? Quotecurrency { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }

        public Order(int id,
                     int userid,
                     OrderKind kind,
                     int? count,
                     decimal? price,
                     int? basecurrencyid,
                     int? quotecurrencyid)
        {
            this.Id = id;
            this.Userid = userid;
            this.Kind = kind;
            this.Count = count;
            this.Price = price;
            this.Basecurrencyid = basecurrencyid;
            this.Quotecurrencyid = quotecurrencyid;
        }
        
        public Order(int userid,
                     OrderKind kind,
                     int? count,
                     decimal? price,
                     int? basecurrencyid,
                     int? quotecurrencyid)
        {
            this.Userid = userid;
            this.Kind = kind;
            this.Count = count;
            this.Price = price;
            this.Basecurrencyid = basecurrencyid;
            this.Quotecurrencyid = quotecurrencyid;
        }

        public Order(OrderDto orderDto)
        {
            Id = orderDto.Id;
            Userid = orderDto.Userid;
            Kind = orderDto.Kind;
            Count = orderDto.Count;
            Price = orderDto.Price;
            Basecurrencyid = orderDto.Basecurrencyid;
            Quotecurrencyid = orderDto.Quotecurrencyid;
        }

        public Order()
        {
            
        }

        public void ChangeData(Order order) 
        {
            this.Userid = order.Userid;
            this.Kind = order.Kind;
            this.Count = order.Count;
            this.Basecurrencyid = order.Basecurrencyid;
            this.Price = order.Price;
            this.Quotecurrencyid = order.Quotecurrencyid;
        }
    }
}
