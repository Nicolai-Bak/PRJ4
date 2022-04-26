using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Filters.Salling;

namespace ExternalApiLibrary.Factory;

public class FoetexProductFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new SallingProductCaller(CreateRequest());
    }

    public IFilter CreateFilter()
    {
        return new SallingProductFilter();
    }

    public IConverter CreateConverter()
    {
        return new SallingProductConverter();
    }
    
    private static IRequest CreateRequest()
	{
		var builder = new SallingRequestBuilder();
		
		builder.AddInfos()
			.AddUnits()
			.AddUnitsOfMeasure()
			.AddStoreData();
		
		return builder.Build();
	}
}