using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ApiApplication.Database.ProductNameStandardize;
using ExternalAPIComponent;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            }).ToList();

            //// Insert stores
            Console.WriteLine("Inserting stores - " + DateTime.Now);
            var storeList = new List<Store>();
            foetexStores.ForEach(s =>
            {
                ConvertedSallingStore convertedStore = (ConvertedSallingStore) s;
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
            convertedProducts.ForEach(p =>
            {
                ConvertedSallingProduct sallingProduct = (ConvertedSallingProduct) p;
                productList.Add(new Product()
                {
                    EAN = sallingProduct.EAN,
                    Name = sallingProduct.Name,
                    Brand = sallingProduct.Brand,
                    Units = sallingProduct.Unit,
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
            convertedProducts.ForEach(p =>
            {
                ConvertedSallingProduct sallingProduct = (ConvertedSallingProduct) p;
                foetexStores.ForEach(s =>
                {
                    ConvertedSallingStore convertedStore = (ConvertedSallingStore) s;
                    productStoreList.Add(new ProductStore()
                    {
                        ProductKey = sallingProduct.EAN,
                        StoreKey = convertedStore.ID,
                        Price = sallingProduct.Stores.First().Value.Price
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
}