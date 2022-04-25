using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public interface IApiFactory
{
    public ICaller CreateCaller();
    public IFilter CreateFilter();
    public IConverter CreateConverter();
}