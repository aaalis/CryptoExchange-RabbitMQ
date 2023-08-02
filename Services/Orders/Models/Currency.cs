using System;
using System.Collections.Generic;

namespace Orders.Models
{
    public class Currency
    {
        public Currency()
        {
            OrderBasecurrencies = new HashSet<Order>();
            OrderQuotecurrencies = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Order> OrderBasecurrencies { get; set; }
        public virtual ICollection<Order> OrderQuotecurrencies { get; set; }
    }
}
