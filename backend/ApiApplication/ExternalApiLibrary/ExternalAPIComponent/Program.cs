using System.Web;
using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Salling;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            SallingProductCaller sallingCaller = new();
            CoopProductCaller coopCaller = new();

            var sallingResult = await sallingCaller.Call(new CoopRequestBuilder().Build());
            var coopResult = await coopCaller.Call(new CoopRequestBuilder().Build());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n ------------ Salling Products ------------ ");
            Console.ResetColor();
            sallingResult.ForEach(obj => Console.WriteLine(JToken.Parse((string) obj).ToString(Formatting.Indented) + "\n"));
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n ------------ Coop Products ------------ ");
            Console.ResetColor();
            coopResult.ForEach(obj => Console.WriteLine(JToken.Parse((string) obj).ToString(Formatting.Indented) + "\n"));

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