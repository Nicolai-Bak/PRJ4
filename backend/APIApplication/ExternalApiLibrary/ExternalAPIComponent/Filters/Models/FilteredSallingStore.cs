using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters.Models;

public class FilteredSallingStore : IFilteredDto
{
    [JsonProperty("sapSiteId")] public int Id { get; set; }
    [JsonProperty("brand")] public string Brand { get; set; }
    [JsonProperty("coordinates")] public List<double> Coordinates { get; set; }
    [JsonProperty("address")] public Address AddressField { get; set; }
    public class Address
    {
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("country")] public string Country { get; set; }
        [JsonProperty("street")] public string Street { get; set; }
        [JsonProperty("zip")] public string Zip { get; set; }
    }
}