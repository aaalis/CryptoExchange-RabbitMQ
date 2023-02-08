using Microsoft.EntityFrameworkCore;

namespace Users.Model
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder.Entity<User>().Property(x=>x.Id).HasColumnName("id");
            modelBuilder.Entity<User>().HasKey(x=>x.Id);
            modelBuilder.Entity<User>().Property(x=>x.Name).HasColumnName("name");
            modelBuilder.Entity<User>().Property(x=>x.Login).HasColumnName("login");
            modelBuilder.Entity<User>().Property(x=>x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x=>x.CreationDateTime).HasColumnName("creationdate");
            modelBuilder.Entity<User>().Property(x=>x.IsDeleted).HasColumnName("isdeleted");

            base.OnModelCreating(modelBuilder);
        }
    }
}