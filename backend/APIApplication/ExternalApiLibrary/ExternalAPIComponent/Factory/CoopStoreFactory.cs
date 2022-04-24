using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public class CoopStoreFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new CoopStoreCaller();
    }

    public IFilter CreateFilter()
    {
        return new CoopStoreFilter();
    }

    public IConverter CreateConverter()
    {
        return new CoopStoreConverter();
    }
}