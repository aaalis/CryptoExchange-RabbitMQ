using Microsoft.EntityFrameworkCore;

namespace gRPC_Server.Model
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {

        }
        public DbSet<CurrencyRate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRate>().ToTable("currancy_rate");

            modelBuilder.Entity<CurrencyRate>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<CurrencyRate>().HasKey(x => x.Id);
            modelBuilder.HasPostgresEnum<OrdersCurrency>();
            modelBuilder.Entity<CurrencyRate>().Property(x => x.Currency).HasColumnName("currency");
            modelBuilder.Entity<CurrencyRate>().Property(x => x.Price).HasColumnName("price");
            modelBuilder.Entity<CurrencyRate>().Property(x => x.DateOfChange).HasColumnName("date_of_change");
            modelBuilder.HasPostgresEnum<ActionType>();
            modelBuilder.Entity<CurrencyRate>().Property(x => x.BackRefAction).HasColumnName("back_ref_action");

            base.OnModelCreating(modelBuilder);
        }
    }
}
