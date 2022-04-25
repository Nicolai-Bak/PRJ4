using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopStoreCaller : ICaller
{
    //private static readonly string[] StoresToRetrieve = { "Kvickly", "SuperBrugsen", "DagliBrugsen", "Irma", "Fakta", "Coop365" };
    private static readonly List<string> StoresToRetrieveTest =
        new List<string> { "Kvickly", "SuperBrugsen", "DagliBrugsen", "Irma", "Fakta", "Coop365" };
    private static readonly string BaseUrl = "https://info.coop.dk/umbraco/surface/Chains/GetAllStores";

    /**
     * Retrieves all stores from the Coop API with a POST request.
     */
    public async Task<List<object>> Call(IRequest request)
    {
        var responses = new List<object>();
        var client = new HttpClient();

        foreach (var store in StoresToRetrieveTest)
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