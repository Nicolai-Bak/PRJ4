using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Factory;

public interface IApiFactory
{
    public ICaller CreateCaller();
    public IFilter CreateFilter();
    public IConverter CreateConverter();
}