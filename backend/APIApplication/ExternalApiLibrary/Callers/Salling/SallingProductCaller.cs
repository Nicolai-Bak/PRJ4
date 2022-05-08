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
    public IRequest Request { get; set; }       

    public SallingProductCaller(IRequest request)
    {
        Request = request;
    }
    public async Task<List<IFilteredDto>> Call()
    {
        var response = await Request.CallAll() ?? new List<object>();

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