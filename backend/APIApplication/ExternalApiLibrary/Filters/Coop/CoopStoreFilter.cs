using System.Diagnostics;
using ExternalApiLibrary.DTO;
using Newtonsoft.Json;
using ExternalApiLibrary.Filters.Interfaces;

namespace ExternalApiLibrary.Filters.Coop;

public class CoopStoreFilter : IFilter
{
    public List<object> Filter(List<object> s)
    {
        var result = new List<object>();

        s.ForEach(response =>
        {
            var deserializedRoot = JsonConvert.DeserializeObject<List<FilteredCoopStore>>(response.ToString());
            if (deserializedRoot != null) result.Add(deserializedRoot);
        });

        return result;
    }
}

[DebuggerDisplay("{Name}, {RetailGroupName}, {Address}")]
public class FilteredCoopStore : IFilteredDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Kardex { get; set; }
    public int Zipcode { get; set; }
    public string City { get; set; }
    public List<double> Location { get; set; }
    public string RetailGroupName { get; set; }
}