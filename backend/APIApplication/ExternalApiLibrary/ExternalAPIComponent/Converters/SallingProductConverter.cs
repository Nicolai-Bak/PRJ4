using ApiApplication;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ExternalApiLibrary.ExternalAPIComponent.Filters;


namespace ExternalApiLibrary.ExternalAPIComponent.Converters;

public class SallingProductConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
        List<FilteredSallingProduct> filteredList = list.Cast<FilteredSallingProduct>().ToList();

        var products = filteredList.Select(product => new ConvertedSallingProduct()
        {
            EAN = Int64.Parse(product.Infos!.Find(info => info.Code == "product_details")!.Items!.Find(item => item.Title == "EAN")!.Value!),
            Name = product.HighlightResults!.ProductName!.Text!,
            Brand = product.HighlightResults!.Brand!.Text!,
            Unit = product.Units!.Value!,
            Measurement = product.UnitsOfMeasure!,
            Stores = product.Stores
        }).ToList();

        return new List<object>(products);
    }
}


public class ConvertedSallingProduct
{
    public Int64 EAN { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Unit { get; set; }
    public string Measurement { get; set; }
    public Dictionary<int, StoreData>? Stores { get; set; }
}