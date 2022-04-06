using ExternalAPIComponent.Callers.Interfaces;

namespace ExternalAPIComponent.Callers.Coop;

public class CoopStoreCaller : ICaller
{
    private static readonly string[] StoresToRetrieve ={ "Kvickly", "SuperBrugsen", "DagliBrugsen", "Irma", "Fakta", "Coop365" };
    private static readonly string BaseUrl = "https://info.coop.dk/umbraco/surface/Chains/GetAllStores";

    public Task<List<object>> Call(IRequest request)
    {
        var responses = new List<object>();
        var client = new HttpClient();

        Array.ForEach(StoresToRetrieve, async store =>
        {
            var payload = new Dictionary<string, string>
            {
                { "PageIndex", "1501" },
                { "chainsToShowStoresFrom", store}
            };

            var content = new FormUrlEncodedContent(payload);
            var response = await client.PostAsync(BaseUrl, content);
            
            responses.Add(await response.Content.ReadAsStringAsync());
        });

        return Task.FromResult(responses);
    }
}