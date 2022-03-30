using System.Runtime.InteropServices.ComTypes;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ExternalAPIComponent;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters;
using Serilog;

namespace ApiApplication.HostedServices
{
    public class FoetexService: IHostedService
    {
        private readonly IPrisninjaDB _db;

        public FoetexService(IServiceProvider sp)
        {

            _db = sp.CreateScope().ServiceProvider.GetRequiredService<IPrisninjaDB>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DoTask();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task DoTask()
        {
            Store s = new Store()
            {
                ID = 9,
                Brand = "f",
                Location_X = 1,
                Location_Y = 2,
                Address = "virk nu",
            };

            await _db.InsertStore(s);

            s.ID = 8;

            await _db.InsertStore(s);



            // Configure logger for start up
            BackendLogger.BuildLogger();

            try
            {
                ///// Products - Salling
                SallingProductCaller productCaller = new();
                SallingRequestBuilder builder = new SallingRequestBuilder();
                builder.AddInfos()
                        .AddUnits()
                        .AddUnitsOfMeasure()
                        .AddStoreData();
                IFilter productFilter = new SallingProductFilter();
                IConverter productConverter = new SallingProductConverter();

                var products = await productCaller.Call(builder.Build());
                var filteredProducts = productFilter.Filter(products);
                var convertedProducts = productConverter.Convert(filteredProducts);

                ///// Stores - Salling
                ICaller storeCaller = new SallingStoreCaller();
                IFilter storeFilter = new SallingStoreFilter();
                IConverter storeConverter = new SallingStoreConverter();

                var stores = await storeCaller.Call(null);
                var filteredStores = storeFilter.Filter(stores);
                var convertedStores = storeConverter.Convert(filteredStores);

                ////// Insert stores
                convertedStores.ForEach(async s =>
                {
                    ConvertedStore convertedStore = (ConvertedStore)s;
                    Store store = new Store()
                    {
                        ID = convertedStore.ID,
                        Brand = convertedStore.Brand,
                        Location_X = convertedStore.Location_X,
                        Location_Y = convertedStore.Location_Y,
                        Address = convertedStore.Address
                    };
                    await _db.InsertStore(store);
                });

                ///// Insert products
                convertedProducts.ForEach(convertedProduct =>
                {
                    ConvertedSallingProduct sallingProduct = (ConvertedSallingProduct)convertedProduct;
                    var product = new Product()
                    {
                        EAN = sallingProduct.EAN,
                        Name = sallingProduct.Name,
                        Brand = sallingProduct.Brand,
                        Unit = sallingProduct.Unit,
                        Measurement = sallingProduct.Measurement
                    };
                    convertedStores.ForEach(async convertedStore =>
                    {
                        Store store = (Store)convertedStore;
                        if (store.Brand == "foetex")
                        {
                            await _db.InsertProduct(product, store.ID, price: sallingProduct.Stores[0].Price);
                        }
                    });
                });
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            

        }
    }
}
