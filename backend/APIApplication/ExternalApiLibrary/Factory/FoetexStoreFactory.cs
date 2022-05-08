using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;

namespace ExternalApiLibrary.Factory;

public class FoetexStoreFactory : IApiFactory
{
    public ICaller CreateCaller(bool overrideBackStop = false)
    {
        return new SallingStoreCaller(null);
    }

    public IConverter CreateConverter()
    {
        return new SallingStoreConverter();
    }
}