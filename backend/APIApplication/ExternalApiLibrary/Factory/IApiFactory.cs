using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Filters.Interfaces;

namespace ExternalApiLibrary.Factory;

public interface IApiFactory
{
    public ICaller CreateCaller();
    public IFilter CreateFilter();
    public IConverter CreateConverter();
}