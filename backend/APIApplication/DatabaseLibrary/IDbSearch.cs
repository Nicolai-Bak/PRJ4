using DatabaseLibrary.Models;

namespace DatabaseLibrary;

public interface IDbSearch
{
    List<int> GetStoresInRange(double x, double y, double range);
    List<Store> GetDataFromStores(List<int> topStores);
    List<Product> GetProductsFromSpecificStores(List<int> storeKeys, string productName, string measurement, bool organic);
}