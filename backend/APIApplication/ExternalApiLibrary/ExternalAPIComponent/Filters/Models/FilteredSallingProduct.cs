using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters.Models;

public class FilteredSallingProduct
{
    [JsonProperty("_highlightResult")] public HighlightResult? HighlightResults { get; set; }
    [JsonProperty("infos")] public List<Info>? Infos { get; set; }
    [JsonProperty("units")] public float? Units { get; set; }
    [JsonProperty("unitsOfMeasure")] public string? UnitsOfMeasure { get; set; }
    [JsonProperty("storeData")] public Dictionary<int, StoreData>? Stores { get; set; }
    [JsonProperty("properties")] public List<string>? Properties { get; set; }

    public class HighlightResult
    {
        [JsonProperty("productName")] public Value? ProductName { get; set; }
        [JsonProperty("brand")] public Value? Brand { get; set; }
        public class Value
        {
            [JsonProperty("value")] public string? Text { get; set; }
        }
    }
    public class Info
    {
        [JsonProperty("code")] public string? Code { get; set; }
        [JsonProperty("title")] public string? Title { get; set; }
        [JsonProperty("items")] public List<Item>? Items { get; set; }
        public class Item
        {
            [JsonProperty("title")] public string? Title { get; set; }
            [JsonProperty("value")] public string? Value { get; set; }
        }
    }
    public class StoreData
    {
        [JsonProperty("price")] public int Price { get; set; }
    }
}