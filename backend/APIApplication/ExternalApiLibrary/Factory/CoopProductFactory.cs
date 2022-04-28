using ExternalApiLibrary.Callers.Coop;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Coop;
using ExternalApiLibrary.Converters.Interfaces;

namespace ExternalApiLibrary.Factory;

public class CoopProductFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new CoopProductCaller(CreateRequest());
    }

    public IConverter CreateConverter()
    {
        return new CoopProductConverter();
    }
    
    private static IRequest CreateRequest()
    {
	    var builder = new CoopRequestBuilder();
	    
		return builder.Build();
    }
}