namespace ApiApplication.Database.Models;

public class Product
{
    public int EAN { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public float Unit { get; set; }
    public string Measurement { get; set; }
    public float Price { get; set; }
    public List<Store> Stores { get; set; }
}

