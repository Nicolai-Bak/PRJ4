namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;

public interface ICaller
{
	public Task<List<object>> Call();
}