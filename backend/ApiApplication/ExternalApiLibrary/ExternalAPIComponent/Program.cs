using System.Web;
using ApiApplication.Database.Models;
using ExternalAPIComponent;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters;
using Serilog;

namespace ExternalApiLibrary.ExternalAPIComponent;

internal static class Program
{
    public static async Task Main()
    {
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

            convertedProducts.ForEach(x =>
            {
                ConvertedSallingProduct y = (ConvertedSallingProduct)x;
                //Product y = (Product)x;
                Console.WriteLine(y.EAN + "\n" + y.Name + "\n" + y.Brand + "\n" + y.Unit + " " + y.Measurement);
                foreach (var keyValuePair in y.Stores!)
                {
                    Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value.Price);
                }
                Console.WriteLine();
            });


            ///// Stores - Salling
            ICaller storeCaller = new SallingStoreCaller();
            IFilter storeFilter = new SallingStoreFilter();
            IConverter storeConverter = new SallingStoreConverter();

            var stores = await storeCaller.Call(null);
            var filteredStores = storeFilter.Filter(stores);
            var convertedStores = storeConverter.Convert(filteredStores);

            convertedStores.ForEach(x =>
            {
                Store y = (Store)x;
                Console.WriteLine(y.ID + "\n" + y.Brand + "\n" + y.Address + "\n" + y.Location_X + ", " + y.Location_Y);
                Console.WriteLine();
            });

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
    }


}