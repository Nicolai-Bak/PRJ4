using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Factory;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
namespace ExternalApiLibrary.ExternalAPIComponent;

public class ExternalApi: IExternalApi
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
    public async Task<List<object>> Get(IRequest request)
    {
        return converter.Convert(filter.Filter(await caller.Call(request)));
    }
}