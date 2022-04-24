using ApiApplication.Database.Models;

namespace ApiApplication.Database;

public interface IDbRequest
{
    List<string> GetAllProductNames();
    ProductStandardName GetProductInfo(string name);
}