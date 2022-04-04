using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database.Data;

public class PrisninjaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("" +
                                    "Server=tcp:prj4server.database.windows.net,1433;" +
                                    "Initial Catalog=PRJ4 Database;" +
                                    "Persist Security Info=False;" +
                                    "User ID=superadmin;" +
                                    "Password=Superpassword1;" +
                                    "MultipleActiveResultSets=False;" +
                                    "Encrypt=True;" +
                                    "TrustServerCertificate=False;" +
                                    "Connection Timeout=30;"
        );

        // optionsBuilder.UseSqlServer("" +
        //                             "Data Source=" + ";" +
        //                             "Database=TestDB;" +
        //                             "TrustServerCertificate=true;" +
        //                             "User ID=SA;" +
        //                             "PASSWORD=<Tofirebananer147>"
        // );
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductStore> ProductStores => Set<ProductStore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>()
            .HasKey(s => s.ID);

        modelBuilder.Entity<Product>()
            .HasKey(p => p.EAN);

        modelBuilder.Entity<ProductStore>()
            .HasKey(ps => new {ps.ProductKey, ps.StoreKey});

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