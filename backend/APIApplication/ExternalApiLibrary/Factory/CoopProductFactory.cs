using ExternalApiLibrary.Callers.Coop;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Coop;
using ExternalApiLibrary.Converters.Interfaces;

namespace ExternalApiLibrary.Factory;

public class CoopProductFactory : IApiFactory
{
    public ICaller CreateCaller(bool overrideBackStop = false)
    {
        return new CoopProductCaller(CreateRequest(overrideBackStop));
    }

    public IConverter CreateConverter()
    {
        return new CoopProductConverter();
    }
    
    private static IRequest CreateRequest(bool overrideBackStop = false)
    {
	    var builder = new CoopRequestBuilder(overrideBackStop);
	    
		return builder.Build();
    }
}