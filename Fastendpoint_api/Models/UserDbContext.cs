using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Fastendpoint_api.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Email = "demoacc@demo.com",
                    Password = "Pa$$word1",
                    Role = "admin",
                    Username = "demoacc",
                    Id = Guid.NewGuid()
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-EMCR7O1C\\SQLEXPRESS;Database=UserDB;Trusted_Connection=True;");
        }

    }
}
