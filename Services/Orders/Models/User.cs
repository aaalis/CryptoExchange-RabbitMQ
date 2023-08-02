using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Orders.Models
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? Creationdate { get; set; }
        public bool Isdeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
