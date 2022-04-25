using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
<<<<<<< HEAD
=======
using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Filters.Salling;

>>>>>>> ExternalApi namespaces updated
namespace ExternalApiLibrary.Factory;

public class FoetexProductFactory : IApiFactory
{
<<<<<<< HEAD
    public ICaller CreateCaller(bool overrideBackStop = false)
    {
        return new SallingProductCaller(CreateRequest(overrideBackStop));
=======
    public ICaller CreateCaller()
    {
        return new SallingProductCaller();
    }

    public IFilter CreateFilter()
    {
        return new SallingProductFilter();
>>>>>>> ExternalApi namespaces updated
    }

    public IConverter CreateConverter()
    {
        return new SallingProductConverter();
    }
<<<<<<< HEAD
    
    private static IRequest CreateRequest(bool overrideBackStop = false)
	{
		var builder = new SallingRequestBuilder(overrideBackStop);
		
		builder.AddInfos()
			.AddUnits()
			.AddUnitsOfMeasure()
			.AddStoreData();
		
		return builder.Build();
	}
=======
>>>>>>> ExternalApi namespaces updated
}