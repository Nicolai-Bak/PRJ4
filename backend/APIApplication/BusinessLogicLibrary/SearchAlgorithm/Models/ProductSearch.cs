using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models;

public class ProductSearch
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Units { get; set; }
    public string Measurement { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public ProductSearch(Product p, double price, int amount)
    {
        Name = p.Name;
        Brand = p.Brand;
        Units = p.Units;
        Measurement = p.Measurement;
        Price = price;
        Amount = amount;
    }
}