namespace ApiApplication.Database.Models;

public class Store
{
    public int ID { get; set; }
    public int Brand { get; set; }
    public double Location_X { get; set; }
    public double Location_Y { get; set; }
    public string Address { get; set; }
    public List<ProductStore> ProductStores { get; set; }
}

