using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

namespace BusinessLogicLibrary.SearchAlgorithm.Models;

public class StoredataAdder: IStoredataAdder
{
    public void AddStoredata(List<Store> storeData, List<StoreSearch> stores, double userLocationX, double userLocationY)
    {
        foreach (Store store in storeData)
        {
            foreach (var storeSearch in stores)
            {
                if (storeSearch.StoreID == store.ID)
                {
                    storeSearch.Address = store.Address;
                    storeSearch.Brand = store.Brand;
                    storeSearch.Distance = GetDistance(store.Location_X, store.Location_Y, userLocationX, userLocationY);
                }
            }
        }
    }
    
    private double GetDistance(double storeX, double storeY, double customerX, double customerY)
    {
        return Math.Sqrt(
            Math.Pow(Math.Abs(storeX - customerX), 2) +
            Math.Pow(Math.Abs(storeY - customerY), 2));
    }
}