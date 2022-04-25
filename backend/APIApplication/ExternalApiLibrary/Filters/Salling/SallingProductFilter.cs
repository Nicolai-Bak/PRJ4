using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Filters.Salling;

public class SallingProductFilter : IFilter
{
    public List<object> Filter(List<object> o)
    {
        List<List<FilteredSallingProduct>> json = JsonConvert.DeserializeObject<List<List<FilteredSallingProduct>>>(JsonConvert.SerializeObject(o));

        //return new List<object>(json);

        var result = new List<object>();

        json.ForEach(j =>
        {
            j.ForEach(s =>
                {
                    result.Add(s);
                });
        });

        return result;
    }
}