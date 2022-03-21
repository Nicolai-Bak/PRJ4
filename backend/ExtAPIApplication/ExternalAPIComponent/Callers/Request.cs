using Algolia.Search.Clients;
using Algolia.Search.Models.Settings;

namespace ExternalAPIComponent.Callers;

public class Request : IRequest
{
    private readonly SearchClient client;
    private readonly SearchIndex index;

    public Request(SearchClient client, SearchIndex index)
    {
        this.client = client;
        this.index = index;
    }

    public void SetParameters(List<string> parameters)
    {
        var settings = new IndexSettings
        {
            AttributesToRetrieve = parameters
        };

        index.SetSettings(settings);
    }
}