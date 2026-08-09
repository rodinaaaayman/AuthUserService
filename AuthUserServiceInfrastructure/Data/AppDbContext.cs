using AuthUserServiceApplication.Interfaces;
using AuthUserServiceDomain.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthUserServiceInfrastructure.Data
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Brokers> Brokers { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Users>()
                .ToTable("Users");

            modelBuilder.Entity<Clients>()
                .ToTable("Clients");

            modelBuilder.Entity<Brokers>()
                .ToTable("Brokers");

            modelBuilder.Entity<Admins>()
                .ToTable("Admins");

            modelBuilder.Entity<RefreshTokens>()
                .HasOne(rt => rt.User)
                .WithOne()
                .HasForeignKey<RefreshTokens>(rt => rt.Id)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
