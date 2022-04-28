using ExternalApiLibrary.Callers.Coop;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Coop;
using ExternalApiLibrary.Converters.Interfaces;

namespace ExternalApiLibrary.Factory;

public class CoopStoreFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new CoopStoreCaller(null);
    }

    public IConverter CreateConverter()
    {
        return new CoopStoreConverter();
    }
}