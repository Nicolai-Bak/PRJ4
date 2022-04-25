using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models;

public class ProductAdder:IProductAdder 
{
    
    public void AddCheapestProductToStores(List<int> storeIDs, List<StoreSearch> stores, List<Product> productToAdd, double amountToGet)
    {
        // Tilføj dee billigste products i butikken til hver store - fjern store hvis den ikke har alle varer
        
            foreach (int storeID in storeIDs.ToList())
            {
                double price = 0;
                int amount = 0;
                Product temp = productToAdd
                    .Where(p => p.ProductStores
                        .Select(ps => ps.StoreKey)
                        .Any(sk => sk == storeID))
                    .MinBy(p => p.ProductStores
                        .Select(ps => GetPrice(amountToGet,ps.Product.Units,ps.Price,ref price,ref amount)));
                //.MinBy(p => p.ProductStores.Select(ps => ps.Price));
                if (temp != null)
                {
                    stores.Find(s => s.StoreID == storeID).Add(new ProductSearch(temp, price, amount));
                }
                else
                {
                    stores.Remove(stores.Find(s => s.StoreID == storeID));
                    storeIDs.Remove(storeID);
                }
            }
        
    }
    
    private double GetPrice(double unitsToGet, double unitsPerPiece, double pricePerPiece, ref double price, ref int amount)
    {
        amount = (int)Math.Ceiling(unitsToGet / unitsPerPiece);
        price = amount * pricePerPiece;
        return price;
    }
}