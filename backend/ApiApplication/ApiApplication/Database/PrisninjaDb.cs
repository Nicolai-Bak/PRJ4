using System.Data.SqlClient;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database;

public class PrisninjaDb
{
    private PrisninjaDbContext _context;

    public PrisninjaDb(PrisninjaDbContext context)
    {
        _context = context;
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
        { }
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
            { }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }

        _context.Add(new ProductStore() { ProductKey = product.EAN, StoreKey = storeId, Price = price });
        
        await _context.Database.OpenConnectionAsync();
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        { }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }
    }
}