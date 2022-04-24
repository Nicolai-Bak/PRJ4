using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

public interface IStoredataAdder
{
    public void AddStoredata(List<Store> storeData, List<StoreSearch> stores, double userLocationX, double userLocationY);
}