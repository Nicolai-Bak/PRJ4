namespace ExternalAPIComponent.Callers.Interfaces;

public interface IRequest
{
    public Task<object> CallPage();
    public Task<List<object>> CallAll();
}