using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLibrary;

public class PrisninjaDb : IDbRequest, IDbSearch, IDbInsert
{
    private PrisninjaDbContext _context;

    public PrisninjaDb(PrisninjaDbContext context)
    {
        _context = context;
    }

    public List<string> GetAllProductNames()
    {
        return _context.ProductStandardNames.Select(sn => sn.Name).ToList();
    }
    
    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }
    
    public ProductStandardName GetProductInfo(string name)
    {
        return _context.ProductStandardNames.FirstOrDefault(sn => sn.Name == name);
    }

    public List<int> GetStoresInRange(double x, double y, double range)
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

    public List<Store> GetDataFromStores(List<int> topStores)
    {
        return _context.Stores
            .Where(s => 
                topStores.Any(t => t == s.ID))
            .ToList();
    }

    public List<Product> GetProductsFromSpecificStores(List<int> storeKeys, string productName, string measurement)
    {
        return _context.Products
            .Select(p => p)
            .Where(p => p.ProductStores
                            .Select(ps => ps.StoreKey)
                            .Any(psk => storeKeys
                                .Any(sk => sk == psk))
                        && p.Name.Contains(productName)
                        && p.Measurement == measurement)
            .Include(p => p.ProductStores)
            .ThenInclude(ps => ps.Store)
            .ToList();
    }

    public void InsertStores(List<Store> stores)
    {
        _context.Stores.BulkInsert(stores, options =>
        {
            options.InsertKeepIdentity = true;
            options.InsertIfNotExists = true;
        });
    }

    public void InsertProducts(List<Product> products)
    {
        _context.Products.BulkInsert(products, options =>
        {
            options.InsertKeepIdentity = true;
            options.InsertIfNotExists = true;
        });
    }

    public void InsertProductStores(List<ProductStore> productStores)
    {
        _context.ProductStores.BulkInsert(productStores, options =>
        {
            options.InsertKeepIdentity = true;
            options.InsertIfNotExists = true;
        });
    }

    public void InsertProductStandardNames(List<ProductStandardName> productStandardNames)
    {
        _context.ProductStandardNames.BulkInsert(productStandardNames, options =>
        {
            options.InsertKeepIdentity = true;
            options.InsertIfNotExists = true;
        });
    }
}