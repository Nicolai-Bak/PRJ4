using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Coop;

public class CoopRequestBuilder : IRequestBuilder
{
	private const string _baseUrl = "https://mad.coop.dk/api/search/products";
	private bool _overrideBackStop = false;

	public CoopRequestBuilder(bool overrideBackStop = false)
	{
		_overrideBackStop = overrideBackStop;
	}

	/**
     * Builds a CoopRequest
     * 
     * Coop does not use any API-keys or app-ids, nor do they support any parameters.
     */
    public IRequest Build()
    {
        return new CoopRequest(_baseUrl, _overrideBackStop);
    }
}