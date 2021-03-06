namespace DatabaseLibrary.Models;

public class Store : IDbModelsDto
{
    public int ID { get; set; }
    public string Brand { get; set; }
    public double Location_X { get; set; }
    public double Location_Y { get; set; }
    public string Address { get; set; }
    public List<ProductStore> ProductStores { get; set; }
}

