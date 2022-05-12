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
    public IDbInsert Db { get; }
    private PeriodicTimer _timer;
    public Dictionary<IExternalApi, IExternalApi> ExternalApis { get; }
    public IProductNameStandardizer _pns { get; }

    /**
     * The backstop for the external API service. This limits the amount of requests that can be made to the external API.
     * 
     * True = API is in production mode. All requests are allowed.
     * False = API is in test mode. Only a limited number of requests are allowed.
     * Never use production mode when testing, as this will likely cause the API to be blocked.
     */
    private bool _overrideBackStop;

    public ExternalApiService(IDbInsert db,
        Dictionary<IApiFactory, IApiFactory> apiFactories,
        IProductNameStandardizer pns,
        bool overrideBackStop = false)
    {
        Db = db;
        _pns = pns;
        _overrideBackStop = overrideBackStop;

        ExternalApis = new Dictionary<IExternalApi, IExternalApi>();
        foreach (var keyValuePair in apiFactories)
        {
            ExternalApis.Add(new ExternalApi(keyValuePair.Key, _overrideBackStop),
                            new ExternalApi(keyValuePair.Value, _overrideBackStop));
        }


    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan interval = TimeSpan.FromDays(1);
        //calculate time to run the first time & delay to set the timer
        var nextRunTime = DateTime.Today.AddHours(25);   //.AddDays(1).AddHours(1);
        var curTime = DateTime.Now;
        var firstInterval = nextRunTime.Subtract(curTime);

        var t1 = Task.Delay(firstInterval, cancellationToken);
        t1.Wait(cancellationToken);

        //now schedule it to be called every 24 hours for future
        _timer = new PeriodicTimer(interval);

        //do Task at expected time
        await UpdateDatabase();

        while (await _timer.WaitForNextTickAsync(cancellationToken))
        {
            await UpdateDatabase();
        }

    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public async Task UpdateDatabase()
    {
        var products = new List<Product>();
        var stores = new List<Store>();
        var productStores = new List<ProductStore>(); ;

        foreach (var api in ExternalApis)
        {
            var p = new List<Product>();
            var s = new List<Store>();

            var productError = false;
            var storeError = false;

            try
            {
                p = (await api.Key.Get()).Cast<Product>().ToList();
            }
            catch (Exception e)
            {
                Log.Fatal(e, $"[ExtAPI Service] Failed to update database - Uncaught exception fetching {nameof(p)}");
                productError = true;
            }

            try
            {
                s = (await api.Value.Get()).Cast<Store>().ToList();
            }
            catch (Exception e)
            {
                Log.Fatal(e, $"[ExtAPI Service] Failed to update database - Uncaught exception fetching {nameof(s)}");
                storeError = true;
            }

            //if (!storeError)
            //{
            //    s = s.GroupBy(s => s.ID).Select(s => s.First()).ToList();
            //}

            if (!productError) products.AddRange(p);
            if (!storeError) stores.AddRange(s);

            if (!productError && !storeError)
            {
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
        }

        products = products.GroupBy(x => x.EAN).Select(y => y.First()).ToList();

        Db.ClearDatabase();                                // Clear database before inserting new data
        Db.InsertStores(stores);                           // Insert stores
        Db.InsertProducts(products);                       // Insert products
        Db.InsertProductStores(productStores);             // Insert productStores
        Db.InsertProductStandardNames(_pns.Standardize(Db.GetAllProducts()));   // Insert product standard names
    }
}
