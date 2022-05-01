using ExternalApiLibrary.DTO;

namespace ExternalApiLibrary.Callers.Interfaces;

public interface ICaller
{
    public IRequest Request { get; set; }
	public Task<List<IFilteredDto>> Call();
}