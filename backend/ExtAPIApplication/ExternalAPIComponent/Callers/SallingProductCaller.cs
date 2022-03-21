using Algolia.Search.Clients;

namespace ExternalAPIComponent.Callers;

public class SallingProductCaller : Caller
{
    private SearchClient client;

    public SallingProductCaller()
    {
        var client = new SearchClient("YourApplicationID", "YourAdminAPIKey");
    }

    public override string Call(IRequestBuilder requestBuilder)
    {
        throw new NotImplementedException();
    }
}