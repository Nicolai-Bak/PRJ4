using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm.Models;
using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

public interface IProductAdder
{
    public void AddCheapestProductToStores(List<int> storeIDs, List<StoreSearch> stores, List<Product> productToAdd, double amountToGet);
}