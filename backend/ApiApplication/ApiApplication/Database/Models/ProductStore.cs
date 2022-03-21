namespace ApiApplication.Database.Models;

public class ProductStore
{
    public Product Product { get; set; }
    public int ProductKey { get; set; }
    public Store Store { get; set; }
    public int StoreKey { get; set; }
    public double Price { get; set; }
}

