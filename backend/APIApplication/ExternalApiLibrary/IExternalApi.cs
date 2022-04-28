using DatabaseLibrary.Models;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;

namespace ExternalApiLibrary;

public interface IExternalApi
{
    public ICaller caller { set; }
    public IConverter converter { set; }
    public Task<List<IDbModelsDto>> Get();
}