using System.Data.Common;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using Orders.Model;

namespace Orders.Model
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Order>().ToTable("orders");
            
            modelBuilder.Entity<Order>().HasKey(x=>x.ID);
            
            modelBuilder.HasPostgresEnum<OrderKind>();
            modelBuilder.HasPostgresEnum<Currency>();
            modelBuilder.Entity<Order>().Property<OrderKind>("order_kind");
            modelBuilder.Entity<Order>().Property<Currency>("order_basecurrency");
            modelBuilder.Entity<Order>().Property<Currency>("order_quotecurrency");

            base.OnModelCreating(modelBuilder);
        }
    }
}