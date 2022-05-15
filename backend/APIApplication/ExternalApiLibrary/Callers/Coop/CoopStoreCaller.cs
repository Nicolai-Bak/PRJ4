using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopStoreCaller : ICaller
{
    public static readonly List<string> StoresToRetrieve =
        new() { "Kvickly", "SuperBrugsen", "DagliBrugsen", "Irma", "Fakta", "Coop365" };
    private static readonly string BaseUrl = "https://info.coop.dk/umbraco/surface/Chains/GetAllStores";

    public IRequest Request { get; set; } = null;

    public CoopStoreCaller(IRequest request)
    { }


    /**
     * Retrieves all stores from the Coop API with a POST request.
     */
    public async Task<List<IFilteredDto>> Call()
    {
        var responses = new List<object>();
        var client = new HttpClient();
        var result = new List<IFilteredDto>();

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

        responses.ForEach(r =>
        {
            var deserializedRoot = JsonConvert.DeserializeObject<List<FilteredCoopStore>>(r.ToString());
            if (deserializedRoot != null)
            {
                result.AddRange(deserializedRoot);
            }
        });

        return result;

    }
}