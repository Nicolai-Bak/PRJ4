namespace DatabaseLibrary.Models;

public class Product : IDbModelsDto
{
    public Int64 EAN { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Units { get; set; }
    public string Measurement { get; set; }
    
    public bool Organic { get; set; }
    public string ImageUrl { get; set; }
    public List<ProductStore> ProductStores { get; set; }
}