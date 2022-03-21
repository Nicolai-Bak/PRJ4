namespace ApiApplication.Database.Models;

public class Store
{
    public int ID { get; set; }
    public int Brand { get; set; }
    public float Location_X { get; set; }
    public float Location_Y { get; set; }
    public string Address { get; set; }
    public List<Product> Products { get; set; }
}

