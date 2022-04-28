using DatabaseLibrary.Models;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Factory;

namespace ExternalApiLibrary;

public class ExternalApi : IExternalApi
{
    public ICaller caller { private get; set; }
    public IConverter converter { private get; set; }

    public ExternalApi(IApiFactory factory)
    {

        caller = factory.CreateCaller();
        converter = factory.CreateConverter();
    }
    public async Task<List<IDbModelsDto>> Get()
    {
        return converter.Convert(await caller.Call());
    }
}
