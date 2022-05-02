namespace ExternalApiLibrary.Callers.Interfaces;

public interface IRequest
{
    public Task<List<object>> CallAll();
}