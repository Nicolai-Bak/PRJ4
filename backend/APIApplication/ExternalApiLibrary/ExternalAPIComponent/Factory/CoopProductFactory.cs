using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public class CoopProductFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        return new CoopProductCaller();
    }

    public IFilter CreateFilter()
    {
        return new CoopProductFilter();
    }

    public IConverter CreateConverter()
    {
        return new CoopProductConverter();
    }
}