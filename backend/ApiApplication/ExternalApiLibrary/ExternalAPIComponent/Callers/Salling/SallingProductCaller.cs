using ExternalApiLibrary.ExternalAPIComponent.Callers.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;

/**
 * Handles calling the API with requests.
 */
public class SallingProductCaller : ICaller
{
    public async Task<List<object>> Call(IRequest request)
    {
        var result = await request.CallAll();

        return result;
    }
}