using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.ProductNameStandardize;

public interface IProductNameStandardizer
{
    List<ProductStandardName> Standardize(List<Product> products);
}