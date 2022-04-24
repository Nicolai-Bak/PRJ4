using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Salling;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public class F�texProductFactory : IApiFactory
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