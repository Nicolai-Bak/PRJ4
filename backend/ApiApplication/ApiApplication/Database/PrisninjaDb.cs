using System.Data.SqlClient;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database;

public interface IPrisninjaDB
{
    List<string> GetAllProductNames();
    List<Product> GetAllProducts();
    void InsertStores(List<Store> stores);
    void InsertProducts(List<Product> products);
    void InsertProductStores(List<ProductStore> productStores);
    void InsertProductStandardNames(List<ProductStandardName> productStandardNames);
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
    
    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
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