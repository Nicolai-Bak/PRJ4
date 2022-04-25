using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Coop;

public class CoopProductCaller : ICaller
{
    public async Task<List<object>> Call(IRequest request)
    {
        var result = await request.CallAll();

        return result;
    }
}