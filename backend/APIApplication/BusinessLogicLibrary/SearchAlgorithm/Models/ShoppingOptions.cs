namespace BusinessLogicLibrary.SearchAlgorithm.Models;

public class ShoppingOptions
{
    public List<StoreSearch> Best { get; set; }
    public List<StoreSearch> Cheapest { get; set; } 
    public List<StoreSearch> Nearest { get; set; }
}