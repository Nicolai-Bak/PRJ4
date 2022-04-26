using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.Filters.Interfaces;

namespace ExternalApiLibrary;

public class ExternalApi : IExternalApi
{
    public ICaller caller { private get; set; }
    public IFilter filter { private get; set; }
    public IConverter converter { private get; set; }

    public ExternalApi(IApiFactory factory)
    {
        caller = factory.CreateCaller();
        filter = factory.CreateFilter();
        converter = factory.CreateConverter();
    }
    public async Task<List<object>> Get()
    {
        return converter.Convert(filter.Filter(await caller.Call()));
    }
}
