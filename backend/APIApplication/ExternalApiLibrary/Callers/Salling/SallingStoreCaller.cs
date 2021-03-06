using System.Web;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Callers.Salling;

public class SallingStoreCaller : ICaller
{
    public IRequest Request { get; set; }
	
	public SallingStoreCaller(IRequest request)
	{
		Request = request;
	}

    public async Task<List<IFilteredDto>> Call()
    {
        string _subscriptionKey = "Bearer c38e62ac-bcb3-43a0-8b10-315a8e117cd1";
        var response = await MakeRequest(_subscriptionKey);

        var result = new List<IFilteredDto>();

            result.AddRange(
                JsonConvert.DeserializeObject<List<FilteredSallingStore>>(response)!);

        return result;
    }

    static async Task<string> MakeRequest(string subKey)
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

        //List<object> responseList = JsonConvert.DeserializeObject<List<object>>(responseString);

        return responseString;
    }
}