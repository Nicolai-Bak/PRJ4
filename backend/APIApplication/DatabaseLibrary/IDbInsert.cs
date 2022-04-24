using ApiApplication.Database.Models;

namespace ApiApplication.Database;

public interface IDbInsert
{
    List<Product> GetAllProducts();
    void InsertStores(List<Store> stores);
    void InsertProducts(List<Product> products);
    void InsertProductStores(List<ProductStore> productStores);
    void InsertProductStandardNames(List<ProductStandardName> productStandardNames);
}