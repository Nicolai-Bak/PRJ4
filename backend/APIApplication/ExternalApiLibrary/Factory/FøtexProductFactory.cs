using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Salling;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public class FoetexProductFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new SallingProductCaller();
    }

    public IFilter CreateFilter()
    {
        return new SallingProductFilter();
    }

    public IConverter CreateConverter()
    {
        return new SallingProductConverter();
    }
}