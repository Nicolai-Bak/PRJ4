using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
namespace ExternalApiLibrary.Factory;

public class FoetexProductFactory : IApiFactory
{
    public ICaller CreateCaller(bool overrideBackStop = false)
    {
        return new SallingProductCaller(CreateRequest(overrideBackStop));
    }

    public IConverter CreateConverter()
    {
        return new SallingProductConverter();
    }
    
    private static IRequest CreateRequest(bool overrideBackStop = false)
	{
		var builder = new SallingRequestBuilder(overrideBackStop);
		
		builder.AddInfos()
			.AddUnits()
			.AddUnitsOfMeasure()
			.AddImages()
			.AddStoreData();
		
		return builder.Build();
	}
}