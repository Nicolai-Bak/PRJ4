using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopRequestBuilder : IRequestBuilder
{
    private readonly string _baseUrl = "https://mad.coop.dk/api/search/products";

    /**
     * Builds a CoopRequest
     * 
     * Coop does not use any API-keys or app-ids, nor do they support any parameters.
     */
    public IRequest Build()
    {
        return new CoopRequest(_baseUrl);
    }
}