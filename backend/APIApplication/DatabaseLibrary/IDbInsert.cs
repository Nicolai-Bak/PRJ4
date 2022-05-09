using DatabaseLibrary.Models;

namespace DatabaseLibrary;

public interface IDbInsert
{
    List<Product> GetAllProducts();
    void InsertStores(List<Store> stores);
    void InsertProducts(List<Product> products);
    void InsertProductStores(List<ProductStore> productStores);
    void InsertProductStandardNames(List<ProductStandardName> productStandardNames);
    void ClearDatabase();
}