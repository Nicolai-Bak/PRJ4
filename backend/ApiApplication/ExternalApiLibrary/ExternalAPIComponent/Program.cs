using System.Web;
using ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Filters;
using Serilog;

namespace ExternalAPIComponent;

internal static class Program
{
    public static async Task Main()
    {
        // Configure logger for start up
        BackendLogger.BuildLogger();

        try
        {
            SallingProductCaller caller = new();
            SallingRequestBuilder builder = new SallingRequestBuilder();
            builder.AddInfos()
                    .AddUnits()
                    .AddUnitsOfMeasure()
                    .AddStoreData();

            var result = await caller.Call(builder.Build());

            //result.ForEach(obj => Console.WriteLine(obj.ToString()));

            IFilter filter = new SallingProductFilter();

            Console.WriteLine(result[0]);
            var filteredResult = filter.Filter(result);

            Console.WriteLine();

            filteredResult.ForEach(x =>
            {
                FilteredSallingProduct y = (FilteredSallingProduct)x;
                Console.WriteLine(y.HighlightResults.ProductName.Text);
                Console.WriteLine(value: y.Infos.Find(info => info.Code == "product_details").Items.Find(item => item.Title == "EAN").Value);
                foreach (var keyValuePair in y.Stores)
                {
                    Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value.Price);
                }
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