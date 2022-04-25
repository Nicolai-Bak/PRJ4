using ExternalApiLibrary.Callers.Coop;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Coop;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Filters.Coop;
using ExternalApiLibrary.Filters.Interfaces;

namespace ExternalApiLibrary.Factory;

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