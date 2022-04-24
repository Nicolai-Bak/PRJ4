using ExternalApiLibrary.ExternalAPIComponent.Callers.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
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