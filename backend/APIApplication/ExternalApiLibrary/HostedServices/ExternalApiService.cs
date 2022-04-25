using BusinessLogicLibrary.ProductNameStandardize;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using ExternalApiLibrary.ExternalAPIComponent;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Factory;
using ExternalApiLibrary.ExternalAPIComponent.Utilities.Logs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ApiApplication.HostedServices;
public class Service : IHostedService
{
    private readonly IDbInsert _db;
    private PeriodicTimer _timer;
    public Service(IServiceProvider sp)
    {
        _db = sp.CreateScope().ServiceProvider.GetRequiredService<IDbInsert>();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan interval = TimeSpan.FromHours(24);
        //calculate time to run the first time & delay to set the timer
        //DateTime.Today gives time of midnight 00.00
        var nextRunTime = DateTime.Today.AddDays(1).AddHours(1);
        var curTime = DateTime.Now;
        var firstInterval = nextRunTime.Subtract(curTime);

        Action action = async () =>
        {
            var t1 = Task.Delay(firstInterval);
            t1.Wait();
            //remove inactive accounts at expected time
            DoTask();
            //now schedule it to be called every 24 hours for future
            // timer repeates call to RemoveScheduledAccounts every 24 hours.
            _timer = new PeriodicTimer(interval);

            while (await _timer.WaitForNextTickAsync())
            {
                await DoTask();
            }

            //_timer = new Timer(
            //    DoTask,
            //    null,
            ///    TimeSpan.Zero,
            //    interval
            //);

        };
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public async Task DoTask()
    {
        // Configure logger for start up
        BackendLogger.BuildLogger();

        //try
        //{
        //    ///// Products - Salling
        //}
        //catch (Exception e)
        //{
        //    Log.Fatal(e, "Application failed to start");
        //}
        //finally
        //{
        //    Log.CloseAndFlush();
        //}

        ExternalApi føtexProductApi = new ExternalApi(new FoetexProductFactory());

        SallingRequestBuilder builder = new SallingRequestBuilder();
        builder.AddInfos()
                .AddUnits()
                .AddUnitsOfMeasure()
                .AddStoreData();

        var products = await føtexProductApi.Get(builder.Build());

        ///// Stores - Salling
        ExternalApi føtexStoreApi = new ExternalApi(new FoetexStoreFactory());

        var stores = await føtexStoreApi.Get(null);

        var foetexStores = stores.Where(store =>
        {
	        Store sallingStore = (Store)store;
            return sallingStore.Brand == "foetex";
        }).ToList();

        //// Insert stores
        Console.WriteLine("Inserting stores - " + DateTime.Now);
        var storeList = new List<Store>();
        foetexStores.ForEach(s =>
        {
	        Store convertedStore = (Store)s;
            storeList.Add(new Store()
            {
                ID = convertedStore.ID,
                Brand = convertedStore.Brand,
                Location_X = convertedStore.Location_X,
                Location_Y = convertedStore.Location_Y,
                Address = convertedStore.Address
            });
        });
        Console.WriteLine("Bulk insert - " + DateTime.Now);
        _db.InsertStores(storeList);

        ///// Insert products
        Console.WriteLine("Inserting Products - " + DateTime.Now);
        var productList = new List<Product>();
        products.ForEach(p =>
        {
	        Product sallingProduct = (Product)p;
            productList.Add(new Product()
            {
                EAN = sallingProduct.EAN,
                Name = sallingProduct.Name,
                Brand = sallingProduct.Brand,
                Units = sallingProduct.Units,
                Measurement = sallingProduct.Measurement,
                Organic = false,
                ImageUrl = ""
            });
        });
        Console.WriteLine("Bulk insert - " + DateTime.Now);
        _db.InsertProducts(productList);

        ///// Insert productStores
        Console.WriteLine("Inserting ProductStores - " + DateTime.Now);
        var productStoreList = new List<ProductStore>();
        products.ForEach(p =>
        {
	        Product sallingProduct = (Product)p;
            foetexStores.ForEach(s =>
            {
	            Store convertedStore = (Store)s;
                productStoreList.Add(new ProductStore()
                {
                    ProductKey = sallingProduct.EAN,
                    StoreKey = convertedStore.ID,
                    Price = sallingProduct.ProductStores.First().Price
                });
            });
        });
        Console.WriteLine("Bulk insert - " + DateTime.Now);
        _db.InsertProductStores(productStoreList);

        Console.WriteLine("DONE! - " + DateTime.Now);

        ProductNameStandardizer pns = new ProductNameStandardizer();
        var standardizedList = pns.Standardize(_db.GetAllProducts());
        _db.InsertProductStandardNames(standardizedList);
    }
}
