using ExternalApiLibrary.Callers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopRequest : IRequest
{
    /**
     * Number of products to receive per request
     * 
     * Maximum products per request is 1000
     * Set to 1 to receive 1 product per page
     */
    private readonly int _pageSize;
    public string BaseUrl { get; set; }

    private int PageIndex { get; set; }

    // True = Production Environment
    // False = Development Environment - limits calls to not overload the external API
    public bool RetrieveAll { get; set; } = false;

    public CoopRequest(string baseUrl, int pageSize = 10)
    {
	    BaseUrl = baseUrl ?? throw new ArgumentNullException();
	    _pageSize = pageSize;
    }

    public async Task<object> CallPage()
    {
        HttpClient client = new HttpClient();

        var url = BaseUrl + $"?pageSize={_pageSize}&page={PageIndex}";
        var content = await client.GetStringAsync(url);

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
            if (!RetrieveAll && PageIndex >= 1)
                continueCondition = false;

        } while (continueCondition);

        // Clean up for the next request
        PageIndex = 0;

        return responses;
    }

    /**
     * Swaps requests to production such that all pages are read
     * 
     * Should never be used in development, as this means this
     * request will retrieve every product
     *
     * Default: Development
     */
    public void SwapToProduction()
    {
        RetrieveAll = true;
    }

    private static bool ResponseContainedProducts(string response)
    {
        dynamic d = JObject.Parse(response);
        return d.products.Count > 0;
    }

}