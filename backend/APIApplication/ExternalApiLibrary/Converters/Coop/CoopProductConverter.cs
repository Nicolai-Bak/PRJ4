using System.Globalization;
using System.Text.RegularExpressions;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Serilog;

namespace ExternalApiLibrary.Converters.Coop;

public class CoopProductConverter : IConverter
{
    /**
	 * Convert the list of products from external API products to internal API products
	 */
    public List<IDbModelsDto> Convert(List<IFilteredDto> list)
    {
        var filteredList = list.Cast<FilteredCoopProduct>().ToList();

        var products = filteredList.Select(p => new Product()
        {
            EAN = long.Parse(p.id),
            Name = p.displayName,
            Brand = p.brand ?? " ",
            Units = GetUnitFromSpotText(p.spotText, GetMeasurementFromSpotText(p.spotText)),
            Measurement = GetMeasurementFromSpotText(p.spotText),
            Organic = IsProductOrganic(p.labels),
            ImageUrl = p.image ?? " ",
            ProductStores = new List<ProductStore>
            {
                new ProductStore
                {
	                // Sales Price is in kr. - Change to øre
                    Price = (long) p.salesPrice.amount * 100,
                }
            }

        }).ToList();

        return ProductValidator.ValidateProducts(products, ProductGroup.Coop);
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
    private static bool IsProductOrganic(List<FilteredCoopProduct.Label> labels)
    {
        string[] organicLabelIds = { "o-market", "eu-okologi" };

        return labels.Any(label => organicLabelIds
            .Any(labelId => labelId == label.id));
    }
}
