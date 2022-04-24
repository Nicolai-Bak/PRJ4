using ApiApplication.Controllers;
using ApiApplication.Database;
using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

public interface IProductAdder
{
    public void AddCheapestProductToStores(List<int> storeIDs, List<StoreSearch> stores, List<Product> productToAdd, double amountToGet);
}