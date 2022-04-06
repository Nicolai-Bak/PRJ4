using System.Web;
//using ApiApplication.Database;
//using ApiApplication.Database.Data;
//using ApiApplication.Database.Models;
using ExternalAPIComponent;
using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Filters;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;
using Serilog;

namespace ExternalApiLibrary.ExternalAPIComponent;

public static class Program
{
    public static async Task Main()
    {
        /*
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

            //convertedProducts.ForEach(x =>
            //{
            //    ConvertedSallingProduct y = (ConvertedSallingProduct)x;
            //    Console.WriteLine(y.EAN + "\n" + y.Name + "\n" + y.Brand + "\n" + y.Unit + " " + y.Measurement);
            //    foreach (var keyValuePair in y.Stores!)
            //    {
            //        Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value.Price);
            //    }
            //    Console.WriteLine();
            //});


            ///// Stores - Salling
            ICaller storeCaller = new SallingStoreCaller();
            IFilter storeFilter = new SallingStoreFilter();
            IConverter storeConverter = new SallingStoreConverter();

            var stores = await storeCaller.Call(null);
            var filteredStores = storeFilter.Filter(stores);
            var convertedStores = storeConverter.Convert(filteredStores);

            //convertedStores.ForEach(x =>
            //{
            //    Store y = (Store)x;
            //    Console.WriteLine(y.ID + "\n" + y.Brand + "\n" + y.Address + "\n" + y.Location_X + ", " + y.Location_Y);
            //    Console.WriteLine();
            //});

            ////// Insert stores
            PrisninjaDb db = new PrisninjaDb(new PrisninjaDbContext());
            convertedStores.ForEach(async store =>
            {
                await db.InsertStore((Store)store);
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
                        await db.InsertProduct(product, store.ID, price: sallingProduct.Stores[0].Price);
                    }
                });
            });
            //PrintSallingProductSample();

            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Application failed to start");
        }
        finally
        {
            Log.CloseAndFlush();
        }
        */
    }

    private static async void PrintCoopProductSample()
    {
        CoopProductCaller coopCaller = new();
        CoopProductFilter coopFilter = new();
        CoopProductConverter coopConverter = new();

        var coopResult = await coopCaller.Call(new CoopRequestBuilder().Build());
        var filteredProducts = coopFilter.Filter(coopResult);
        var convertedProducts = coopConverter.Convert(filteredProducts);
        
        convertedProducts.ForEach(x =>
        {
            var y = (ConvertedCoopProduct) x;
            Console.WriteLine(y.EAN + "\n" + y.Name + "\n" + y.Brand + "\n" + y.Unit + " " + y.Measurement);
            Console.WriteLine();
        });

        //Console.ForegroundColor = ConsoleColor.Red;
        //Console.WriteLine("\n\n ------------ Coop Products ------------ ");
        //Console.ResetColor();
        //coopResult.ForEach(obj => Console.WriteLine(JToken.Parse((string) obj).ToString(Formatting.Indented) + "\n"));
    }
    
    private static async void PrintSallingProductSample()
    {
        SallingProductCaller sallingCaller = new();
        var sallingResult = await sallingCaller.Call(new SallingRequestBuilder().Build());
            
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n\n ------------ Salling Products ------------ ");
        Console.ResetColor();
        Console.WriteLine(sallingResult[0].ToString());
        //sallingResult.ForEach(obj => Console.WriteLine(JToken.Parse((string) obj).ToString(Formatting.Indented) + "\n"));
    }


}