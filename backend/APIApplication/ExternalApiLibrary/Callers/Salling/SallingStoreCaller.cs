using System.Web;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;

public class SallingStoreCaller : ICaller
{
    public async Task<List<object>> Call(IRequest request)
    {
        string _subscriptionKey = "Bearer c38e62ac-bcb3-43a0-8b10-315a8e117cd1";
        var result = await MakeRequest(_subscriptionKey);
        return result;
    }

    static async Task<List<object>> MakeRequest(string subKey)
    {
        var client = new HttpClient();
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        Console.WriteLine(queryString);

        // Request headers
        client.DefaultRequestHeaders.Add("Authorization", subKey);

        var uri = "https://api.sallinggroup.com/v2/stores?fields=sapSiteId,brand,coordinates,address&country=dk&per_page=1000" + queryString;

        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();

        List<object> responseList = JsonConvert.DeserializeObject<List<object>>(responseString);

        return responseList;
    }
}