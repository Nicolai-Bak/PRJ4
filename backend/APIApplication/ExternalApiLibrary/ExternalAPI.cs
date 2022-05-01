using DatabaseLibrary.Models;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Factory;

namespace ExternalApiLibrary;

public class ExternalApi : IExternalApi
{
    public ICaller Caller { private get; set; }
    public IConverter Converter { private get; set; }

    public ExternalApi(IApiFactory factory, bool overrideBackStop = false)
    {
	    Caller = factory.CreateCaller(overrideBackStop);
        Converter = factory.CreateConverter();
    }
    public async Task<List<IDbModelsDto>> Get()
    {
        return Converter.Convert(await Caller.Call());
    }
}
