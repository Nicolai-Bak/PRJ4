using BusinessLogicLibrary.ProductNameStandardize;
using DatabaseLibrary;
using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Factory;
using Serilog;

namespace ExternalApiLibrary;

public static class Program
{
    public static async Task Main()
    {
        IDbInsert _db = new PrisninjaDb(new PrisninjaDbContext());
        bool _overrideBackStop = false;

        var products = new List<Product>();
        var stores = new List<Store>();
        var productStores = new List<ProductStore>();

        // Products - Foetex
        var foetexProductApi = new ExternalApi(new FoetexProductFactory(), _overrideBackStop);
        var foetexProducts = (await foetexProductApi.Get()).Cast<Product>().ToList();

        // Stores - Foetex
        var foetexStoreApi = new ExternalApi(new FoetexStoreFactory(), _overrideBackStop);
        var foetexStores = (await foetexStoreApi.Get()).Cast<Store>().Where(s => s.Brand == "foetex").ToList();

        products.AddRange(foetexProducts);
        stores.AddRange(foetexStores);

        foetexProducts.ForEach(p =>
        {
            foetexStores.ForEach(s =>
            {
                productStores.Add(new ProductStore()
                {
                    ProductKey = p.EAN,
                    StoreKey = s.ID,
                    Price = p.ProductStores.First().Price
                });
            });
        });

        // Products - Coop
        var coopProductApi = new ExternalApi(new CoopProductFactory(), _overrideBackStop);
        var coopProducts = (await coopProductApi.Get()).Cast<Product>().ToList();
        // Stores - Coop
        var coopStoreApi = new ExternalApi(new CoopStoreFactory(), _overrideBackStop);
        var coopStores = (await coopStoreApi.Get()).Cast<Store>().ToList();

        products.AddRange(coopProducts);
        stores.AddRange(coopStores);

        coopProducts.ForEach(p =>
        {
            coopStores.ForEach(s =>
            {
                productStores.Add(new ProductStore()
                {
                    ProductKey = p.EAN,
                    StoreKey = s.ID,
                    Price = p.ProductStores.First().Price
                });
            });
        });

        _db.InsertStores(stores);               // Insert stores
        _db.InsertProducts(products);           // Insert products
        _db.InsertProductStores(productStores); // Insert productStores

        ProductNameStandardizer pns = new ProductNameStandardizer();
        var standardizedList = pns.Standardize(_db.GetAllProducts());
        _db.InsertProductStandardNames(standardizedList);
    }
}