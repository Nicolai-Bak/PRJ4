using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;

public class CoopProductFilter : IFilter
{
    public List<object> Filter(List<object> o)
    {
        var result = new List<object>();
        
        o.ForEach(response =>
        {
            var deserializedRoot = JsonConvert.DeserializeObject<Root>((string) response);
            if (deserializedRoot != null) result.Add(deserializedRoot.products);
        });
        
        return result;
    }
}

public class Price
{
    public string separator { get; set; }
    public double amount { get; set; }
    public string formattedAmount { get; set; }
    public string formattedAmountLong { get; set; }
    public string minor { get; set; }
    public string major { get; set; }
}
public class DiscountLabel
{
    public int minQuantity { get; set; }
    public Price price { get; set; }
    public bool isMix { get; set; }
    public object savedPercentage { get; set; }
    public bool hasNewSalesPrice { get; set; }
    public int discountLabelType { get; set; }
    public bool showSavings { get; set; }
    public string name { get; set; }
    public object usageLimitPerOrder { get; set; }
    public object defaultQuantity { get; set; }
}

public class Label
{
    public string id { get; set; }
    public string parentId { get; set; }
    public string displayName { get; set; }
    public int priority { get; set; }
}

public class SalesPrice
{
    public string separator { get; set; }
    public double amount { get; set; }
    public string formattedAmount { get; set; }
    public string formattedAmountLong { get; set; }
    public string minor { get; set; }
    public string major { get; set; }
}

public class FilteredCoopProduct
{
    public string id { get; set; }
    public string displayName { get; set; }
    public string category { get; set; }
    public string image { get; set; }
    public string brand { get; set; }
    public DiscountLabel discountLabel { get; set; }
    public string spotText { get; set; } // Contains weight information
    public List<Label> labels { get; set; }
    public SalesPrice salesPrice { get; set; }
    public object bundleProducts { get; set; }
    public object originalPrice { get; set; }
    public string pricePerUnitText { get; set; }
    public object contentsText { get; set; }
}

public class Root
{
    public List<FilteredCoopProduct> products { get; set; }
}