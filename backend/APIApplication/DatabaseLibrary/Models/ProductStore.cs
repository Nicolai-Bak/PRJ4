namespace DatabaseLibrary.Models;

public class ProductStore
{
    public Product Product { get; set; }
    public Int64 ProductKey { get; set; }
    public Store Store { get; set; }
    public int StoreKey { get; set; }
    public long Price { get; set; }
}

