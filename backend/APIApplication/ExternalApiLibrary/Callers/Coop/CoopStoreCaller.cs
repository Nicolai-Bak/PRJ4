using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopStoreCaller : ICaller
{
    private static readonly List<string> StoresToRetrieve =
        new() { "Kvickly", "SuperBrugsen", "DagliBrugsen", "Irma", "Fakta", "Coop365" };
    private static readonly string BaseUrl = "https://info.coop.dk/umbraco/surface/Chains/GetAllStores";
    private IRequest _request;
    
    public CoopStoreCaller(IRequest request)
    {
		_request = request;
    }
    
    /**
     * Retrieves all stores from the Coop API with a POST request.
     */
    public async Task<List<object>> Call()
    {
        var responses = new List<object>();
        var client = new HttpClient();

        foreach (var store in StoresToRetrieve)
        {
            var payload = new Dictionary<string, string>
            {
                {"pageId", "1501"},
                {"chainsToShowStoresFrom", store}
            };

            var content = new FormUrlEncodedContent(payload);
            var response = client.PostAsync(BaseUrl, content);

            var responseString = await response.Result.Content.ReadAsStringAsync();

            responses.Add(responseString);
        }

        return responses;
    }
}