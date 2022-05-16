using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;

namespace ExternalApiLibrary.Converters.Salling;

public class SallingProductConverter : IConverter
{
    public List<IDbModelsDto> Convert(List<IFilteredDto> list)
    {
        var filteredList = list.Cast<FilteredSallingProduct>().ToList();

        var products = filteredList.Select(product => new Product()
        {
            EAN = long.Parse(product.Infos!.Find(info => info.Code == "product_details")!.Items!.Find(item => item.Title == "EAN")!.Value!),
            Name = product.HighlightResults!.ProductName!.Text!,
            Brand = product.HighlightResults!.Brand!.Text ?? " ",
            Units = Math.Round(product.Units!.Value, 4),
            Measurement = product.UnitsOfMeasure!,
            Organic = IsProductOrganic(product.HighlightResults!.ProductName!.Text!, product.Properties),
            ImageUrl = product.Image[0] ?? " ",
            ProductStores = new List<ProductStore> {new ProductStore
            {
                Price = product.Stores!.First().Value.Price
            }},
        }).ToList();

        return ProductValidator.ValidateProducts(products, ProductGroup.Salling);
    }

    /**
     * Checks if the product is organic
     */
    private static bool IsProductOrganic(string productName, List<string>? properties)
    {
        return DoesPropertiesContainOrganic(properties) || DoesProductNameContainOrganic(productName);
    }

    private static bool DoesProductNameContainOrganic(string name)
    {
        string[] organicWords = { "økologisk", "øko" };

        return organicWords.Any(word => name.ToLower().Contains(word));
    }

    private static bool DoesPropertiesContainOrganic(List<string>? properties)
    {
        if (properties == null || properties.Count == 0)
            return false;

        string[] organicProperties = { "økologisk" };

        return properties.Any(property => organicProperties.Contains(property.ToLower()));
    }
}