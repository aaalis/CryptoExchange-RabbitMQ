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
            
            // modelBuilder.Entity<Order>().HasKey(x=>x.ID);


            modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.OwnerGuid).HasColumnName("ownerguid");
            modelBuilder.HasPostgresEnum<OrderKind>();
            modelBuilder.Entity<Order>().Property<OrderKind>(x => x.Kind).HasColumnName("kind");
            modelBuilder.Entity<Order>().Property(x => x.Count).HasColumnName("count");
            modelBuilder.Entity<Order>().Property(x => x.Price).HasColumnName("price");
            modelBuilder.HasPostgresEnum<Currency>();
            modelBuilder.Entity<Order>().Property<Currency>(x => x.BaseCurrency).HasColumnName("basecurrency");
            modelBuilder.Entity<Order>().Property<Currency>(x => x.QuoteCurrency).HasColumnName("quotecurrency");

            base.OnModelCreating(modelBuilder);
        }
    }
}