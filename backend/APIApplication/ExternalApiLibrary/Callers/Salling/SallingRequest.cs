using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using ExternalApiLibrary.Callers.Interfaces;

namespace ExternalApiLibrary.Callers.Salling;

/**
 * Handles requesting the API for products with given attributes to retrieve.
 */
public class SallingRequest : IRequest
{
    /**
     * Used by Algolia to search their APIs
     */
    private readonly SearchIndex _index;

    /**
     * Number of products to receive per request
     * 
     * Maximum products per request is 1000
     * Set to 1 to receive 1 product per page
     */
    private int _pageSize = 60;

    /**
     * Limits the amount of pages read
     * 
     * Use low values (10-20) for development,
     * as this value increases the number
     * of calls made to the API.
     */
    private readonly int _maxPages = 2;
    private int _pageIndex { get; set; }

    public List<string> Parameters = new();

    public SallingRequest(SearchIndex index)
    {
        _index = index;
    }

    public async Task<List<object>> CallPage()
    {
        var response = await _index.SearchAsync<object>(new Query("")
        {
            AttributesToRetrieve = Parameters,
            HitsPerPage = _pageSize,
            Page = _pageIndex
        });

        // Only uncomment the line below if every product needs to be called for
        // if (response.NbPages != _maxPages) _maxPages = response.NbPages;

        return response.Hits;
    }

    public async Task<List<object>> CallAll()
    {
        List<object> responses = new();

        do
        {
            responses.Add(await CallPage());
            _pageIndex++;
        } while (_pageIndex != _maxPages);

        // Clean up for subsequent calls
        _pageIndex = 0;

        return responses;
    }
}