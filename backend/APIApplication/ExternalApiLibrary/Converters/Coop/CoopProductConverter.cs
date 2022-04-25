using System.Globalization;
using System.Text.RegularExpressions;
<<<<<<< HEAD
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
=======
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Filters.Coop;
>>>>>>> ExternalApi namespaces updated
using Serilog;

namespace ExternalApiLibrary.Converters.Coop;

public class CoopProductConverter : IConverter
{
    /**
	 * Convert the list of products from external API products to internal API products
	 */
    public List<IDbModelsDto> Convert(List<IFilteredDto> list)
    {
<<<<<<< HEAD
        var filteredList = list.Cast<FilteredCoopProduct>().ToList();

        //var flattenedList = (from flatList in filteredList
        //                     from item in flatList
        //                     select item).ToList();

        var Products = new List<IDbModelsDto>();

        filteredList.ForEach(p =>
            Products.Add(new Product
            {
                EAN = long.Parse(p.id),
                Name = p.displayName,
                Brand = p.brand,
                Units = double.Parse(p.spotText),
                Measurement = GetMeasurementFromSpotText(p.spotText),
                Organic = IsProductOrganic(p.labels),
                ImageUrl = p.image,
                ProductStores = new List<ProductStore>
                {
                    new ProductStore
                    {
                        Price = p.salesPrice.amount,
                    }
                }
            }));

        return Products;
=======
        var filteredList = list.Cast<List<FilteredCoopProduct>>().ToList();

        var flattenedList = (from flatList in filteredList
                             from item in flatList
                             select item).ToList();

        var convertedProducts = new List<object>();

        flattenedList.ForEach(product =>
            convertedProducts.Add(new ConvertedCoopProduct
            {
                EAN = long.Parse(product.id),
                Name = product.displayName,
                Brand = product.brand,
                Unit = GetUnitFromSpotText(product.spotText, GetMeasurementFromSpotText(product.spotText)),
                Measurement = GetMeasurementFromSpotText(product.spotText),
                Organic = IsProductOrganic(product.labels)
            }));

        return new List<object>(convertedProducts);
>>>>>>> ExternalApi namespaces updated
    }

    /**
     * Returns the measurement from the spot text.
     */
    private static string GetMeasurementFromSpotText(string spotText)
    {
        string[] unitsToMatch = { "kg", " g,", " g ", "stk", "liter" };

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

    /**
     * Returns the unit from the spot text.
     */
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

    /**
     * Checks if the product is organic
     */
<<<<<<< HEAD
    private static bool IsProductOrganic(List<FilteredCoopProduct.Label> labels)
=======
    private static bool IsProductOrganic(List<Label> labels)
>>>>>>> ExternalApi namespaces updated
    {
        string[] organicLabelIds = { "o-market", "eu-okologi" };

        return labels.Any(label => organicLabelIds
            .Any(labelId => labelId == label.id));
    }
}
<<<<<<< HEAD
=======

/**
 * Class to hold the converted product
 */
public class ConvertedCoopProduct
{
    public long EAN { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Unit { get; set; }
    public string Measurement { get; set; }
    public bool Organic { get; set; }
}
>>>>>>> ExternalApi namespaces updated
