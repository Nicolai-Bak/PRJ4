using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Callers.Salling;

/**
 * Handles calling the API with requests.
 */
public class SallingProductCaller : ICaller
{
    private IRequest _request;

    public SallingProductCaller(IRequest request)
    {
        _request = request;
    }
    public async Task<List<IFilteredDto>> Call()
    {
        var response = await _request.CallAll() ?? new List<object>();

        List<List<FilteredSallingProduct>> json =
            JsonConvert.DeserializeObject<List<List<FilteredSallingProduct>>>(JsonConvert.SerializeObject(response));

        var result = new List<IFilteredDto>();

        json.ForEach(j =>
        {
            result.AddRange(j);
        });

        return result;
    }
}