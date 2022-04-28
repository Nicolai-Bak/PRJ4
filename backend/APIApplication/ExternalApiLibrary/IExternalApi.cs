using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Filters.Interfaces;

namespace ExternalApiLibrary;

public interface IExternalApi
{
    public ICaller caller { set; }
    public IConverter converter { set; }
    public Task<List<object>> Get();
}