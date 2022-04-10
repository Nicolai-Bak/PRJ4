using System.Globalization;
using System.Text.RegularExpressions;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;
using Serilog;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;

public class CoopProductConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
        var filteredList = list.Cast<List<FilteredCoopProduct>>().ToList();
        
        var flattenedList = (from flatList in filteredList
            from item in flatList
            select item).ToList();
        
        var convertedProducts = new List<object>();
        
        flattenedList.ForEach(product => 
            convertedProducts.Add(new ConvertedCoopProduct
            {
                EAN = Int64.Parse(product.id),
                Name = product.displayName,
                Brand = product.brand,
                Unit = GetUnitFromSpotText(product.spotText, GetMeasurementFromSpotText(product.spotText)),
                Measurement = GetMeasurementFromSpotText(product.spotText)
            }));
        
        return new List<object>(convertedProducts);
    }

    private static string GetMeasurementFromSpotText(string spotText)
    {
        string[] unitsToMatch =  { "kg", " g,", " g ", "stk", "liter" };
        
        var matchingUnits = unitsToMatch
            .Where(spotText.Contains)
            .ToList();

        switch (matchingUnits.Count)
        {
            case 0:
                Log.Fatal(
                    "Coop product conversion failed " +
                    "- Product spot text contained no unit of measurement: " +
                    $"SpotText: {spotText}"
                );
                return "";
            case > 1:
                Log.Fatal(
                    "Coop product conversion failed " +
                    "- Product spot text contained too many units of measurement: " +
                    $"SpotText: {spotText}, matched units: {string.Join(", ", matchingUnits)}"
                );
                return matchingUnits.First();
            default:
                return matchingUnits.First();
        }
    }

    private static double GetUnitFromSpotText(string spotText, string measurement)
    {
        // Extracts the value behind the measurement substring
        var unitRegex = new Regex($@"((\d*\.?\d+)\s*({measurement}))");
        
        // To match the unit it needs to be in dot separated format
        var match = unitRegex.Match(spotText.Replace(",", "."));

        // Successful extractions are always present in group 2
        if (match.Success)
            return double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
        
        Log.Fatal("Coop product conversion failed " + 
                  "- Not able to interpret unit in product spot text: " + 
                  $"spotText: {spotText}");
        return 0.0;
    }
}

public class ConvertedCoopProduct
{
    public Int64 EAN { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Unit { get; set; }
    public string Measurement { get; set; }
}