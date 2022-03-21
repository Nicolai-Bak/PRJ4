using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database.Data;

public class PrisninjaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("" +
                                    "Data Source=localhost;" +
                                    "Database=TestDB;" +
                                    "TrustServerCertificate=true;" +
                                    "User ID=SA;" +
                                    "PASSWORD=<Tofirebananer147>"
        );
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>()
            .HasKey(s => s.ID);

        modelBuilder.Entity<Product>()
            .HasKey(p => p.EAN);

        modelBuilder.Entity<Store>()
            .HasMany<Product>(s => s.Products)
            .WithMany(p => p.Stores);
    }
}