using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Factory;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
namespace ExternalApiLibrary.ExternalAPIComponent;

public class ExternalApi: IExternalApi
{
    public ICaller caller { private get; set; }
    public IConverter converter { private get; set; }

    public ExternalApi(IApiFactory factory)
    {
        caller = factory.CreateCaller();
        converter = factory.CreateConverter();
    }
    public async Task<List<object>> Get()
    {
        return converter.Convert(await caller.Call());
    }
}
