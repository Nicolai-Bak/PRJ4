using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Salling;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public class FoetexStoreFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new SallingStoreCaller();
    }

    public IFilter CreateFilter()
    {
        return new SallingStoreFilter();
    }

    public IConverter CreateConverter()
    {
        return new SallingStoreConverter();
    }
}