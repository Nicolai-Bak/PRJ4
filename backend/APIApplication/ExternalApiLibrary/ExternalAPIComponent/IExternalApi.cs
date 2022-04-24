using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent;

public interface IExternalApi
{
    public ICaller caller { set; }
    public IFilter filter { set; }
    public IConverter converter { set; }
    public Task<List<object>> Get(IRequest request);
}