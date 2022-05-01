using DatabaseLibrary.Models;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;

namespace ExternalApiLibrary;

public interface IExternalApi
{
    public ICaller Caller { set; }
    public IConverter Converter { set; }
    public Task<List<IDbModelsDto>> Get();
}