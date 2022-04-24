using ApiApplication.Database.Models;

namespace ApiApplication.Database;

public interface IDbSearch
{
    List<int> GetStoresInRange(double x, double y, int range);
    List<Store> GetDataFromStores(List<int> topStores);
    List<Product> GetProductsFromSpecificStores(List<int> storeKeys, string productName);
}