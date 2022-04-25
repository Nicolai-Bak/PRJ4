namespace ExternalApiLibrary.Callers.Interfaces;

public interface ICaller
{
    public Task<List<object>> Call(IRequest request);
}