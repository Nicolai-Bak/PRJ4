using DatabaseLibrary.Models;

namespace DatabaseLibrary;

public interface IDbRequest
{
    List<string> GetAllProductNames();
    ProductStandardName GetProductInfo(string name);
}