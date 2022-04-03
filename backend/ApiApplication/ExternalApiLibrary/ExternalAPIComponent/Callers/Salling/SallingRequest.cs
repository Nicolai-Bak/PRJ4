using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using ExternalAPIComponent.Callers.Interfaces;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;

/**
 * Handles requesting the API for products with given attributes to retrieve.
 */
public class Request : IRequest
{
    private readonly SearchIndex _index;

    public List<string> Parameters = new();

    public Request(SearchIndex index)
    {
        _index = index;
    }

    /**
     * Number of products to receive per request
     * Maximum products per request is 1000
     */
    private int _pageSize { get; } = 1000;

    private int _maxPages { get; } = 1000;
    private int _page { get; set; }

    public async Task<object> CallPage()
    {
        var response = await _index.SearchAsync<object>(new Query("")
        {
            AttributesToRetrieve = Parameters,
            HitsPerPage = _pageSize,
            Page = _page
        });

        //if (response.NbPages != _maxPages) _maxPages = response.NbPages;

        return response.Hits[0];
    }

    public async Task<List<object>> CallAll()
    {
        List<object> responses = new();

        do
        {
            responses.Add(await CallPage());
            _page++;
        } while (_page != _maxPages);

        return responses;
    }
}