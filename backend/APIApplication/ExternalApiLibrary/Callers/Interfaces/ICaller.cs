using ExternalApiLibrary.DTO;

namespace ExternalApiLibrary.Callers.Interfaces;

public interface ICaller
{
	public Task<List<IFilteredDto>> Call();
}