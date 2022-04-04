using System.Data.SqlClient;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database;

public interface IPrisninjaDB
{
    List<string> GetAllProductNames();
    void InsertStores(List<Store> stores);
    void InsertProducts(List<Product> products);
    void InsertProductStores(List<ProductStore> productStores);
    Task SaveChangesProducts();
    Task SaveChangesStores();
}

public class PrisninjaDb : IPrisninjaDB
{
    private PrisninjaDbContext _context;

    public PrisninjaDb(PrisninjaDbContext context)
    {
        _context = context;
    }

    public List<string> GetAllProductNames()
    {
        return _context.Products.Select(p => p.Name).ToList();
    }

    public List<int> GetStoresInRange(double x, double y, int range)
    {
        return _context.Stores
            .Select(s => s)
            .Where(s =>
                (Math.Sqrt(
                    Math.Pow(Math.Abs(s.Location_X - x), 2) +
                    Math.Pow(Math.Abs(s.Location_Y - y), 2)) < range))
            .Select(s => s.ID)
            .ToList();
    }

    public List<Product> GetProductsFromSpecificStores(List<int> storeKeys, string productName)
    {
        return _context.Products
            .Select(p => p)
            .Where(p => p.ProductStores
                .Select(ps => ps.StoreKey)
                .Any(psk => storeKeys
                    .Any(sk => sk == psk))
                        && p.Name.Contains(productName))
            .ToList();
    }

    public void InsertStores(List<Store> stores)
    {
        _context.Stores.BulkInsert(stores);
    }

    public void InsertProducts(List<Product> products)
    {
        _context.Products.BulkInsert(products);
    }

    public void InsertProductStores(List<ProductStore> productStores)
    {
        _context.ProductStores.BulkInsert(productStores);
    }

    public async Task SaveChangesProducts()
    {
        await _context.Database.OpenConnectionAsync();
        try
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT Products ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT Products OFF");
        }
        catch (DbUpdateException ex)
        {
        }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }
    } 
    public async Task SaveChangesStores()
    {
        await _context.Database.OpenConnectionAsync();
        try
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT Stores ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT Stores OFF");
        }
        catch (DbUpdateException ex)
        {
        }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }
    }
}