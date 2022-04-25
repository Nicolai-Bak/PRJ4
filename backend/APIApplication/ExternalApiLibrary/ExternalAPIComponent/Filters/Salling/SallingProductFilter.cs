using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters.Salling;

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