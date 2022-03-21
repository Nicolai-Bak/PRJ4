using Algolia.Search.Clients;

namespace ExternalAPIComponent;

public class SallingProductCaller : ICaller
{
    private SearchClient client;
    
    public SallingProductCaller()
    {
        SearchClient client = new SearchClient("YourApplicationID", "YourAdminAPIKey");
    }
    
    public string Call()
    {
        throw new NotImplementedException();
    }
}