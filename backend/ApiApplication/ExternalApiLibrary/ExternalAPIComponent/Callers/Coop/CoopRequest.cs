using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Coop;

public class CoopRequest : IRequest
{
    /**
     * Number of products to receive per request
     * 
     * Maximum products per request is 1000
     * Set to 1 to receive 1 product per page
     */
    private readonly int _pageSize = 10;
    public string BaseUrl { get; set; }

    private int PageIndex { get; set; }

    // True = Production Environment
    // False = Development Environment - limits calls to not overload the external API
    private bool _overrideBackStop = false;

    public CoopRequest(string baseUrl, bool overrideBackStop = false)
    {
	    BaseUrl = baseUrl ?? throw new ArgumentNullException();
	    _overrideBackStop = overrideBackStop;
	    if (overrideBackStop)
			_pageSize = 1000;
    }
    
    private async Task<object> CallPage()
    {
        var client = new HttpClient();
        
        var url = BaseUrl + $"?pageSize={_pageSize}&page={PageIndex}";
        
        string content;
        try
        {
	        content = await client.GetStringAsync(url);
        }
        catch (Exception e)
        {
	        Console.WriteLine(e);
	        throw;
        }

        return content;
    }

    public async Task<List<object>> CallAll()
    {
        var responses = new List<object>();
        bool continueCondition;

        // Continues to request pages until an empty products page is reached
        do
        {
            var latestResponse = await CallPage();
            continueCondition = ResponseContainedProducts((string)latestResponse);

            PageIndex++;

            if (continueCondition)
                responses.Add(latestResponse);

            // Limits the amount of calls made to the API
            // Only used in development environment
            if (!_overrideBackStop && PageIndex >= 1)
                continueCondition = false;

        } while (continueCondition);

        // Clean up for the next request
        PageIndex = 0;

        return responses;
    }

    private static bool ResponseContainedProducts(string response)
    {
        dynamic d = JObject.Parse(response);
        return d.products.Count > 0;
    }

}