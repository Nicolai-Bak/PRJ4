using ApiApplication.Database;
using ApiApplication.Database.Models;
using ExternalAPIComponent;
using ExternalApiLibrary.ExternalAPIComponent;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Factory;
using Serilog;

namespace ApiApplication.HostedServices;
public class Service : IHostedService
{
    private readonly IPrisninjaDB _db;
    private PeriodicTimer _timer;
    public Service(IServiceProvider sp)
    {
        _db = sp.CreateScope().ServiceProvider.GetRequiredService<IPrisninjaDB>();
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

        ExternalApi føtexProductApi = new ExternalApi(new FøtexProductFactory());

        SallingRequestBuilder builder = new SallingRequestBuilder();
        builder.AddInfos()
                .AddUnits()
                .AddUnitsOfMeasure()
                .AddStoreData();

        var products = await føtexProductApi.Get(builder.Build());

        ///// Stores - Salling
        ExternalApi føtexStoreApi = new ExternalApi(new FøtexStoreFactory());

        var stores = await føtexStoreApi.Get(null);

        var foetexStores = stores.Where(store =>
        {
            ConvertedSallingStore sallingStore = (ConvertedSallingStore)store;
            return sallingStore.Brand == "foetex";
        });

        ////// Insert stores
        //foreach (var s in convertedStores )
        //{
        //    ConvertedSallingStore convertedStore = (ConvertedSallingStore)s;
        //    Store store = new Store()
        //    {
        //        ID = convertedStore.ID,
        //        Brand = convertedStore.Brand,
        //        Location_X = convertedStore.Location_X,
        //        Location_Y = convertedStore.Location_Y,
        //        Address = convertedStore.Address
        //    };
        //    await _db.InsertStore(store);
        //}

        ///// Insert products
        foreach (var p in products)
        {
            ConvertedSallingProduct sallingProduct = (ConvertedSallingProduct)p;
            var product = new Product()
            {
                EAN = sallingProduct.EAN,
                Name = sallingProduct.Name,
                Brand = sallingProduct.Brand,
                Unit = sallingProduct.Unit,
                Measurement = sallingProduct.Measurement
            };
            //foreach (var s in convertedStores)
            foreach (var s in foetexStores)
            {
                ConvertedSallingStore foetexStore = (ConvertedSallingStore)s;
                var store = new Store()
                {
                    ID = foetexStore.ID,
                    Brand = foetexStore.Brand,
                    Location_X = foetexStore.Location_X,
                    Location_Y = foetexStore.Location_Y,
                    Address = foetexStore.Address,
                };

                await _db.InsertProduct(product, store.ID, sallingProduct.Stores.First().Value.Price);
            }
        }
    }
}
