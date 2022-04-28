using BusinessLogicLibrary.ProductNameStandardize;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ExternalApiLibrary.HostedServices;
public class ExternalApiService : IHostedService
{
    private readonly IDbInsert _db;
    private PeriodicTimer _timer;
    public ExternalApiService(IServiceProvider sp)
    {
        _db = sp.CreateScope().ServiceProvider.GetRequiredService<IDbInsert>();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan interval = TimeSpan.FromHours(24);
        //calculate time to run the first time & delay to set the timer
        var nextRunTime = DateTime.Today.AddDays(1).AddHours(1);
        var curTime = DateTime.Now;
        var firstInterval = nextRunTime.Subtract(curTime);

        Action action = async () =>
        {
            var t1 = Task.Delay(firstInterval);
            t1.Wait();
            //do Task at expected time
            await DoTask();
            //now schedule it to be called every 24 hours for future
            _timer = new PeriodicTimer(interval);

            while (await _timer.WaitForNextTickAsync())
            {
                await DoTask();
            }
        };
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public async Task DoTask()
    {
        var products = new List<Product>();
        var stores = new List<Store>();
        var productStores = new List<ProductStore>();

        // Products - Foetex
        var foetexProductApi = new ExternalApi(new FoetexProductFactory());
        products.AddRange((await foetexProductApi.Get()).Cast<Product>().ToList());

        // Stores - Foetex
        var foetexStoreApi = new ExternalApi(new FoetexStoreFactory());
        stores.AddRange((await foetexStoreApi.Get()).Cast<Store>().Where(s => s.Brand == "foetex").ToList());

        // Products - Coop
        var coopProductApi = new ExternalApi(new CoopProductFactory());
        products.AddRange((await coopProductApi.Get()).Cast<Product>().ToList());

        // Stores - Coop
        var coopStoreApi = new ExternalApi(new CoopStoreFactory());
        stores.AddRange((await coopStoreApi.Get()).Cast<Store>().ToList());

        _db.InsertStores(stores);       // Insert stores

        _db.InsertProducts(products);   // Insert products

        // Insert productStores
        products.ForEach(p =>
        {
            stores.ForEach(s =>
            {
                productStores.Add(new ProductStore()
                {
                    ProductKey = p.EAN,
                    StoreKey = s.ID,
                    Price = p.ProductStores.First().Price
                });
            });
        });
        _db.InsertProductStores(productStores);

        ProductNameStandardizer pns = new ProductNameStandardizer();
        var standardizedList = pns.Standardize(_db.GetAllProducts());
        _db.InsertProductStandardNames(standardizedList);
    }
}
