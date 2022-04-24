using ExternalAPIComponent.Callers.Interfaces;

namespace ExternalAPIComponent.Callers.Coop;

public class CoopRequestBuilder : IRequestBuilder
{
	private const string BaseUrl = "https://mad.coop.dk/api/search/products";

	/**
     * Builds a CoopRequest
     * 
     * Coop does not use any API-keys or app-ids, nor do they support any parameters.
     */
    public IRequest Build()
    {
        return new CoopRequest(BaseUrl);
    }
}