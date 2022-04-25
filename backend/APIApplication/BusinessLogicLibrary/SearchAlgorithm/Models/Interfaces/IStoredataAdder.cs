
using ApiApplication.SearchAlgorithm.Models;
using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

public interface IStoredataAdder
{
    public void AddStoredata(List<Store> storeData, List<StoreSearch> stores, double userLocationX, double userLocationY);
}