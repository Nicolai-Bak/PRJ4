<<<<<<< HEAD
using DatabaseLibrary.Models;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Factory;
=======
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.Filters.Interfaces;
>>>>>>> ExternalApi namespaces updated

namespace ExternalApiLibrary;

public class ExternalApi : IExternalApi
{
    public ICaller caller { private get; set; }
    public IConverter converter { private get; set; }

    public ExternalApi(IApiFactory factory, bool overrideBackStop = false)
    {
	    caller = factory.CreateCaller(overrideBackStop);
        converter = factory.CreateConverter();
    }
    public async Task<List<IDbModelsDto>> Get()
    {
        return converter.Convert(await caller.Call());
    }
}
