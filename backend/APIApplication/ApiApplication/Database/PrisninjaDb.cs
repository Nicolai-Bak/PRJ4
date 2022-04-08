using System.Data.SqlClient;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database;

// Skal vi have noget interface seperation her?? Tænker der måske er to funktionaliteter - ændring af database og læsning af database
public interface IPrisninjaDB
{
    List<string> GetAllProductNames();
    Task InsertStore(Store store);
    Task InsertProduct(Product product, int storeId, double price);

    List<int> GetStoresInRange(double x, double y, int range);

    List<Store> GetDataFromStores(List<int> topStores);

    List<Product> GetProductsFromSpecificStores(List<int> storeKeys, string productName);
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

    //Lavet for at få resten af store data når de rigtige butikker er udvalgt
    public List<Store> GetDataFromStores(List<int> topStores)
    {
        return _context.Stores
            .Where(s => 
                topStores.Any(t => t == s.ID))
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

    public async Task InsertStore(Store store)
    {
        _context.Add(store);

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

    public async Task InsertProduct(Product product, int storeId, double price)
    {
        if (!_context.Products.Contains(product))
        {
            _context.Add(product);

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

        var productStore = new ProductStore() { ProductKey = product.EAN, StoreKey = storeId, Price = price };

        if (!_context.ProductStores.Contains(productStore))
        {
            _context.Add(productStore);

            await _context.Database.OpenConnectionAsync();
            try
            {
                await _context.SaveChangesAsync();
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
}