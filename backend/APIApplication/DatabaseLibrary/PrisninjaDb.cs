using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLibrary;

public class PrisninjaDb : IDbRequest, IDbSearch, IDbInsert
{
    private PrisninjaDbContext _context;
    private readonly IRangeCalculator _rangeCalculator;

    public PrisninjaDb(PrisninjaDbContext context, IRangeCalculator rangeCalculator)
    {
        _context = context;
        _rangeCalculator = rangeCalculator;
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
            .AsEnumerable()
            .Where(s =>
                (_rangeCalculator.Distance(x,s.Location_X,y,s.Location_Y) < range))
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

    public List<Product> GetProductsFromSpecificStores(List<int> storeKeys, string productName, string measurement, bool organic)
    {
        return _context.Products
            .Where(p => p.ProductStores
                            .Select(ps => ps.StoreKey)
                            .Any(psk => storeKeys
                                .Any(sk => sk == psk))
                        && p.Name.Contains(productName)
                        && (p.Measurement == measurement ||
                            p.Measurement.ToLower().Contains('g') && measurement.ToLower().Contains('g') ||
                            p.Measurement.ToLower().Contains('l') && measurement.ToLower().Contains('l')) 
                        &&  (!organic || p.Organic)) 
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

    public void ClearDatabase()
    {
        _context.ProductStores.BulkDelete(_context.ProductStores.Select(x=>x));
        _context.Products.BulkDelete(_context.Products);
        _context.Stores.BulkDelete(_context.Stores);
        _context.ProductStandardNames.BulkDelete(_context.ProductStandardNames);
    }
}