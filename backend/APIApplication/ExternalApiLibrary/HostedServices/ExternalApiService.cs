using BusinessLogicLibrary.ProductNameStandardize;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ExternalApiLibrary.HostedServices;
public class ExternalApiService : IHostedService
{
    private readonly IDbInsert _db;
    private PeriodicTimer _timer;

    /**
     * The backstop for the external API service. This limits the amount of requests that can be made to the external API.
     * 
     * True = API is in production mode. All requests are allowed.
     * False = API is in test mode. Only a limited number of requests are allowed.
     * Never use production mode when testing, as this will likely cause the API to be blocked.
     */
    private bool _overrideBackStop = false;

    private readonly IExternalApi _foetexProductApi;
    private readonly IExternalApi _foetexStoreApi;
    private readonly IExternalApi _coopProductApi;
    private readonly IExternalApi _coopStoreApi;

    private ProductNameStandardizer _pns;

    public ExternalApiService(IServiceProvider sp)
    {
        _db = sp.CreateScope().ServiceProvider.GetRequiredService<IDbInsert>();

        _foetexProductApi = new ExternalApi(new FoetexProductFactory(), _overrideBackStop);
        _foetexStoreApi = new ExternalApi(new FoetexStoreFactory(), _overrideBackStop);
        _coopProductApi = new ExternalApi(new CoopProductFactory(), _overrideBackStop);
        _coopStoreApi = new ExternalApi(new CoopStoreFactory(), _overrideBackStop);

        _pns = new ProductNameStandardizer();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan interval = TimeSpan.FromDays(7);
        //calculate time to run the first time & delay to set the timer
        var nextRunTime = DateTime.Today.AddDays(1).AddHours(1);
        var curTime = DateTime.Now;
        var firstInterval = nextRunTime.Subtract(curTime);

        Action action = async () =>
        {
            var t1 = Task.Delay(firstInterval);
            t1.Wait();
            //do Task at expected time
            await UpdateDatabase();
            //now schedule it to be called every 24 hours for future
            _timer = new PeriodicTimer(interval);

            while (await _timer.WaitForNextTickAsync())
            {
                await UpdateDatabase();
            }
        };
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public async Task UpdateDatabase()
    {
        var products = new List<Product>();
        var stores = new List<Store>();
        var productStores = new List<ProductStore>();

        // Products - Foetex
        var foetexProducts = (await _foetexProductApi.Get()).Cast<Product>().ToList();

        // Stores - Foetex
        var foetexStores = (await _foetexStoreApi.Get()).Cast<Store>().Where(s => s.Brand == "foetex").ToList();

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
        var coopProducts = (await _coopProductApi.Get()).Cast<Product>().ToList();
        // Stores - Coop
        var coopStores = (await _coopStoreApi.Get()).Cast<Store>().ToList();

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

        _db.InsertStores(stores);                           // Insert stores
        _db.InsertProducts(products);                       // Insert products
        _db.InsertProductStores(productStores);             // Insert productStores
        
        // Standard names
        var standardizedList = _pns.Standardize(_db.GetAllProducts());
        _db.InsertProductStandardNames(standardizedList);   // Insert product standard names
    }
}
