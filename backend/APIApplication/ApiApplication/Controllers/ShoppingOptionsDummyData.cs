using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
using DatabaseLibrary.Models;

namespace ApiApplication.Controllers;

public class ShoppingOptionsDummyData: IShoppingOptions
{
    public List<StoreSearch> Cheapest { get; set; } 
    public List<StoreSearch> Nearest { get; set; }
    public List<StoreSearch> Best { get; set; }

    public ShoppingOptionsDummyData()
    {
        ProductSearch product1 = new ProductSearch(new Product
        {
            Name = "Vindruer", Brand = "Kellogs", Units = 100, Measurement = "g"
        }, 15.50, 2);

        ProductSearch product2 = new ProductSearch(new Product
        {
            Name = "Vindruer", Brand = "Kohberg", Units = 200, Measurement = "g"
        }, 18.50, 1);

        ProductSearch product3 = new ProductSearch(new Product
        {
            Name = "Kalkunpålæg", Brand = "Arla", Units = 100, Measurement = "g"
        }, 12.50, 1);

        ProductSearch product4 = new ProductSearch(new Product
        {
            Name = "Rosmarin", Brand = "Lego", Units = 15, Measurement = "g"
        }, 31, 1);

        StoreSearch store1 = new StoreSearch {StoreID = 1, Address = "Finlandsgade 2", Distance = 2, Brand = "Aldi"};
        StoreSearch store2 = new StoreSearch {StoreID = 2, Address = "Katrinebjergvej 2", Distance = 3, Brand = "Meny"};
        StoreSearch store3 = new StoreSearch {StoreID = 3, Address = "Norgegade 2", Distance = 0.5, Brand = "Aldi"};
        StoreSearch store4 = new StoreSearch {StoreID = 4, Address = "Congogade 2", Distance = 0.1, Brand = "Fakta"};
        StoreSearch store5 = new StoreSearch {StoreID = 5, Address = "Nordkoreagade 2", Distance = 4, Brand = "Futex"};

        store1.Add(product1);
        store1.Add(product3);
        store1.Add(product4);

        store2.Add(product1);
        store2.Add(product3);
        store2.Add(product4);

        store3.Add(product1);
        store3.Add(product3);
        store3.Add(product4);

        store4.Add(product2);
        store4.Add(product3);
        store4.Add(product4);

        store5.Add(product2);
        store5.Add(product3);
        store5.Add(product4);
        
        Cheapest.Add(store4);
        Cheapest.Add(store5);
        Cheapest.Add(store1);
        Cheapest.Add(store2);
        Cheapest.Add(store3);
        
        Nearest.Add(store4);
        Nearest.Add(store3);
        Nearest.Add(store1);
        Nearest.Add(store2);
        Nearest.Add(store5);
        
        Best.Add(store4);
        Best.Add(store3);
        Best.Add(store1);
        Best.Add(store2);
        Best.Add(store5);
    }

}