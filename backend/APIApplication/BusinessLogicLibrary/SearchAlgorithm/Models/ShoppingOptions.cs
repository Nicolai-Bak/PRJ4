using ApiApplication.SearchAlgorithm.Models;

namespace ApiApplication.Controllers;

public class ShoppingOptions
{
    public List<StoreSearch> Best { get; set; }
    public List<StoreSearch> Cheapest { get; set; } 
    public List<StoreSearch> Nearest { get; set; }
}