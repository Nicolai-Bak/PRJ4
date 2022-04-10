using System.Runtime.InteropServices.ComTypes;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ExternalAPIComponent;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Filters;
using Serilog;

namespace ApiApplication.HostedServices
{
    public class FoetexService : IHostedService
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

            SallingProductCaller productCaller = new();
            SallingRequestBuilder builder = new SallingRequestBuilder();
            builder.AddInfos()
                    .AddUnits()
                    .AddUnitsOfMeasure()
                    .AddStoreData();

            var products = await productCaller.Call(builder.Build());

            IFilter productFilter = new SallingProductFilter();
            IConverter productConverter = new SallingProductConverter();
            var filteredProducts = productFilter.Filter(products);
            var convertedProducts = productConverter.Convert(filteredProducts);

            ///// Stores - Salling
            ICaller storeCaller = new SallingStoreCaller();
            IFilter storeFilter = new SallingStoreFilter();
            IConverter storeConverter = new SallingStoreConverter();

            var stores = await storeCaller.Call(null);
            var filteredStores = storeFilter.Filter(stores);
            var convertedStores = storeConverter.Convert(filteredStores);

            var foetexStores = convertedStores.Where(store =>
            {
                ConvertedSallingStore sallingStore = (ConvertedSallingStore) store;
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
            foreach (var p in convertedProducts)
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

                    //if (store.Brand == "foetex")
                    //{
                    //    await _db.InsertProduct(product, store.ID, sallingProduct.Stores.First().Value.Price);
                    //}

                    await _db.InsertProduct(product, store.ID, sallingProduct.Stores.First().Value.Price);
                }
            }
        }
    }
}
