using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Filters.Salling;

namespace ExternalApiLibrary.Factory;

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