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

public class FoetexStoreFactory : IApiFactory
{
<<<<<<< HEAD
    public ICaller CreateCaller(bool overrideBackStop = false)
    {
        return new SallingStoreCaller(null);
=======
    public ICaller CreateCaller()
    {
        return new SallingStoreCaller(null);
    }

    public IFilter CreateFilter()
    {
        return new SallingStoreFilter();
>>>>>>> ExternalApi namespaces updated
    }

    public IConverter CreateConverter()
    {
        return new SallingStoreConverter();
    }
}