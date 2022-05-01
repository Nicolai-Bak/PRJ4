using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopProductCaller : ICaller
{
    public IRequest Request { get; set; }
    public CoopProductCaller(IRequest request)
    {
        Request = request;
    }
    public async Task<List<IFilteredDto>> Call()
    {
	    var response = await Request.CallAll() ?? new List<object>();
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