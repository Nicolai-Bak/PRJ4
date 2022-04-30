using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;

namespace ExternalApiLibrary.Factory;

public interface IApiFactory
{
    public ICaller CreateCaller(bool overrideBackStop = false);
    public IConverter CreateConverter();
}