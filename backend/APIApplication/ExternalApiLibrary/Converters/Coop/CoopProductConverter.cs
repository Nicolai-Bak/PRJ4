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
        var filteredList = ValidateProducts(list.Cast<FilteredCoopProduct>().ToList());

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
                    Price = p.salesPrice.amount,
                }
            }

        }).ToList();

        return new List<IDbModelsDto>(products);
    }

    private List<FilteredCoopProduct> ValidateProducts(List<FilteredCoopProduct>? products)
    {
	    var validProducts = new List<FilteredCoopProduct>();
	    
	    products?.ForEach(p =>
	    {
		    bool isValid = true;
		    
		    if (string.IsNullOrWhiteSpace(p.id) || long.Parse(p.id) == 0)
		    {
			    LogInvalidProduct(p, $"Ean was 0");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(p.displayName) || string.IsNullOrWhiteSpace(p.displayName))
		    {
			    LogInvalidProduct(p, $"Name was empty");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(p.brand) || string.IsNullOrWhiteSpace(p.brand))
		    {
			    LogInvalidProduct(p, $"Brand was empty");
			    isValid = false;
		    }
		    if (GetUnitFromSpotText(p.spotText, GetMeasurementFromSpotText(p.spotText)) <= 0)
		    {
			    LogInvalidProduct(p, $"Units was less than or equal to zero");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(GetMeasurementFromSpotText(p.spotText)) 
		        || string.IsNullOrWhiteSpace(GetMeasurementFromSpotText(p.spotText)))
		    {
			    LogInvalidProduct(p, $"Measurement was empty");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(p.image) || string.IsNullOrWhiteSpace(p.image))
		    {
			    LogInvalidProduct(p, $"ImageUrl was empty");
			    isValid = false;
		    }
		    if (p.salesPrice.amount * 100 <= 0)
		    {
			    LogInvalidProduct(p, $"Price was less than or equal to zero");
			    isValid = false;
		    }


		    if (isValid)
			    validProducts.Add(p);
	    });

	    return validProducts;
    }

    private void LogInvalidProduct(FilteredCoopProduct product, string msg)
    {
	    Log.Fatal("[CoopProductConverter] Invalid product conversion error: {@msg}\nProduct: {@product}",
		    msg, product);
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
