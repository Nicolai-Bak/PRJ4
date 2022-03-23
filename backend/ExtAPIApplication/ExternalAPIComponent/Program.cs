using System.Web;
using Serilog;

namespace ExternalAPIComponent;

internal static class Program
{
    public static void Main()
    {
        // Configure logger for start up
        BackendLogger.BuildLogger();

        try
        {
            //MakeRequest();
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

    private static async void MakeRequest()
    {
        var client = new HttpClient();
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        var credentials = Credentials.Instance;

        // Request headers
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", credentials.Keys["Coop"]);

        var uri = "https://api.cl.coop.dk/assortmentapi/v1/product/1290?" + queryString;

        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }
}