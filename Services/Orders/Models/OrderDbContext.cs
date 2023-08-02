using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Orders.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");
        //     }
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currencies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");
                
                modelBuilder.HasPostgresEnum<OrderKind>();

                modelBuilder.Entity<Order>().Property<OrderKind>(x => x.Kind).HasColumnName("kind");

                entity.Property(e => e.Basecurrencyid).HasColumnName("basecurrencyid");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quotecurrencyid).HasColumnName("quotecurrencyid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Basecurrency)
                    .WithMany(p => p.OrderBasecurrencies)
                    .HasForeignKey(d => d.Basecurrencyid)
                    .HasConstraintName("basecurrencyid_id");

                entity.HasOne(d => d.Quotecurrency)
                    .WithMany(p => p.OrderQuotecurrencies)
                    .HasForeignKey(d => d.Quotecurrencyid)
                    .HasConstraintName("quotecurrencyid_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("userid_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Creationdate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creationdate");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
