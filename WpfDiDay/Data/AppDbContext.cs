using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WpfDiDay.Models;

namespace WpfDiDay.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=WPFAppLearnDB.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Food: depenedent;        User: principal
            modelBuilder.Entity<Food>()
                .HasOne(f => f.User) // 1 Food -> 1 User
                .WithMany(u => u.Foods) // 1 User -> Many Foods
                .HasForeignKey(f => f.UserID) // UserID - Foreign key
                .OnDelete(DeleteBehavior.Cascade); // Cascade: delete User -> Delete related Food rows.
        }
    }
}
