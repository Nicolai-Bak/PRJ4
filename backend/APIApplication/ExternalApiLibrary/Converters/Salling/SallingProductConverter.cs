﻿using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Filters.Models;

namespace ExternalApiLibrary.Converters.Salling;

public class SallingProductConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
        List<FilteredSallingProduct> filteredList = list.Cast<FilteredSallingProduct>().ToList();

        var products = filteredList.Select(product => new Product()
        {
            EAN = long.Parse(product.Infos!.Find(info => info.Code == "product_details")!.Items!.Find(item => item.Title == "EAN")!.Value!),
            Name = product.HighlightResults!.ProductName!.Text!,
            Brand = product.HighlightResults!.Brand!.Text!,
            Units = product.Units!.Value,
            Measurement = product.UnitsOfMeasure!,
            ProductStores = new List<ProductStore> {new ProductStore
            {
                Price = product.Stores!.First().Value.Price
            }},
            //Stores = product.Stores,
            Organic = IsProductOrganic(product.HighlightResults!.ProductName!.Text!, product.Properties)
        }).ToList();

        return new List<object>(products);
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


//public class ConvertedSallingProduct
//{
//    public Int64 EAN { get; set; }
//    public string Name { get; set; }
//    public string Brand { get; set; }
//    public double Unit { get; set; }
//    public string Measurement { get; set; }
//    public Dictionary<int, FilteredSallingProduct.StoreData>? Stores { get; set; }
//    public bool Organic { get; set; }
//}