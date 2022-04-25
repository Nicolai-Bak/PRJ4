using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopProductCaller : ICaller
{
    private IRequest _request;
    public CoopProductCaller(IRequest request)
    {
        _request = request;
    }
    public async Task<List<IFilteredDto>> Call()
    {
	    var response = await _request.CallAll() ?? new List<object>();
	    var result = new List<IFilteredDto>();
        response.ForEach(r =>
        {
            var deserializedRoot = JsonConvert.DeserializeObject<RootFilteredCoopProduct>((string)r);
            if (deserializedRoot != null)
            {
                result.AddRange(deserializedRoot.products);
            }
        });
        return result;
    }
}