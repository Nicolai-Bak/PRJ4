using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters
{
    public class SallingStoreFilter : IFilter
    {
        public List<object> Filter(List<object> o)
        {
            List<FilteredSallingStore> json = JsonConvert.DeserializeObject<List<FilteredSallingStore>>(JsonConvert.SerializeObject(o))!;

            return new List<object>(json);
        }
    }

    public class FilteredSallingStore
    {
        [JsonProperty("sapSiteId")] public int Id { get; set; }
        [JsonProperty("brand")] public string Brand { get; set; }
        [JsonProperty("coordinates")] public List<Coordinate> Coordinates { get; set; }
        [JsonProperty("address")] public Address Address { get; set; }
    }
    public class Coordinate
    {
        public double Value { get; set; }
    }
    public class Address
    {
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("street")] public string Street { get; set; }
        [JsonProperty("zip")] public string Zip { get; set; }
    }
}
