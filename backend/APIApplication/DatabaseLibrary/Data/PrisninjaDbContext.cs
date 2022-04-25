using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLibrary.Data;

public class PrisninjaDbContext : DbContext
{
    // public PrisninjaDbContext(DbContextOptions options) : base(options)
    // {
    // }
    //
    // protected PrisninjaDbContext()
    // {
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("" +
                                    "Server=tcp:prisninjadb.database.windows.net,1433;" + 
                                    "Initial Catalog=PrisninjaWebApiDb;" + 
                                    "Persist Security Info=False;" + 
                                    "User ID=PrisninjaDb;" + 
                                    "Password=PRJ4Server;" + 
                                    "MultipleActiveResultSets=False;" + 
                                    "Encrypt=True;" +
                                    "TrustServerCertificate=False;" + 
                                    "Connection Timeout=30;"
        );
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductStore> ProductStores => Set<ProductStore>();
    public DbSet<ProductStandardName> ProductStandardNames => Set<ProductStandardName>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>()
            .HasKey(s => s.ID);

        modelBuilder.Entity<Product>()
            .HasKey(p => p.EAN);

        modelBuilder.Entity<ProductStore>()
            .HasKey(ps => new {ps.ProductKey, ps.StoreKey});

        modelBuilder.Entity<ProductStandardName>()
            .HasKey(psn => psn.Name);

        modelBuilder.Entity<ProductStore>()
            .HasOne<Product>(ps => ps.Product)
            .WithMany(p => p.ProductStores)
            .HasForeignKey(ps => ps.ProductKey);
        modelBuilder.Entity<ProductStore>()
            .HasOne<Store>(ps => ps.Store)
            .WithMany(s => s.ProductStores)
            .HasForeignKey(ps => ps.StoreKey);
    }
}