using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Salling;

/**
 * Handles calling the API with requests.
 */
public class SallingProductCaller : ICaller
{
	private IRequest _request;
	
	public SallingProductCaller(IRequest request)
	{
		_request = request;
	}
	public async Task<List<object>> Call()
    {
        var result = await _request.CallAll();

        return result;
    }
}