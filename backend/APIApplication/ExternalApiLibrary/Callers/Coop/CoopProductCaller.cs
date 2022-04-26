using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopProductCaller : ICaller
{
	private IRequest _request;
	
	public CoopProductCaller(IRequest request)
	{
		_request = request;
	}
	
    public async Task<List<object>> Call()
    {
        var result = await _request.CallAll();

        return result;
    }
}