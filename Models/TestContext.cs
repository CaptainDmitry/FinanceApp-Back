using Microsoft.EntityFrameworkCore;


namespace TestApi.Models
{
    public class TestContext : DbContext
    {
        public DbSet<User> users { get; set; } = null!;
        public TestContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestDB;Username=postgres;Password=Admin");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
        }


    }
}
