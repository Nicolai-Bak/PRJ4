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
    private readonly List<IExternalApi[]> _externalApis;
    private readonly ProductNameStandardizer _pns;

    /**
     * The backstop for the external API service. This limits the amount of requests that can be made to the external API.
     * 
     * True = API is in production mode. All requests are allowed.
     * False = API is in test mode. Only a limited number of requests are allowed.
     * Never use production mode when testing, as this will likely cause the API to be blocked.
     */
    private bool _overrideBackStop;

    public ExternalApiService(IDbInsert db, List<IApiFactory[]> apiFactories, ProductNameStandardizer pns, bool overrideBackStop = false)
    {
        _db = db;
        _pns = pns;
        _overrideBackStop = overrideBackStop;

        _externalApis = new List<IExternalApi[]>();
        apiFactories.ForEach(apiFactory =>
        {
            _externalApis.Add(new ExternalApi[2]
            {
                new ExternalApi(apiFactory[0], _overrideBackStop),
                new ExternalApi(apiFactory[1], _overrideBackStop)

            });
        });
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

        foreach (var api in _externalApis)
        {
            var p = (await api[0].Get()).Cast<Product>().ToList();
            var s = (await api[1].Get()).Cast<Store>().ToList();
            s = s.Where(s => s.ID != 1305 &&
                             s.ID != 1315 &&
                             s.ID != 1370 &&
                             s.ID != 1326 &&
                             s.ID != 1330 &&
                             s.ID != 1350).ToList();    

            products.AddRange(p);
            stores.AddRange(s);

            p.ForEach(p =>
            {
                s.ForEach(s =>
                {
                    productStores.Add(new ProductStore()
                    {
                        ProductKey = p.EAN,
                        StoreKey = s.ID,
                        Price = p.ProductStores.First().Price
                    });
                });
            });
        }

        _db.ClearDatabase();                                // Clear database before inserting new data
        _db.InsertStores(stores);                           // Insert stores
        _db.InsertProducts(products);                       // Insert products
        _db.InsertProductStores(productStores);             // Insert productStores

        // Standard names
        var standardizedList = _pns.Standardize(_db.GetAllProducts());
        _db.InsertProductStandardNames(standardizedList);   // Insert product standard names
    }
}
