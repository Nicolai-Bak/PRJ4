using System.Runtime.InteropServices;
using ApiApplication.Database;
using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm;
using ApiApplication.SearchAlgorithm.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IPrisninjaDB _db;

        public RequestController(IPrisninjaDB db)
        {
            _db = db;
        }


        [HttpGet("/names")]
        public List<string> GetProductNames()
        {
            return _db.GetAllProductNames();
        }
        
        [HttpGet("/productinfo/{name}")]
        public ProductStandardName GetProductInfo(string name)
        {
            return _db.GetProductInfo(name);
        }

        [HttpPost("/options")]
        public async Task<ShoppingOptions> GetOptions(ShoppingList shoppingList)
        {
            CheapestSearcher search = new CheapestSearcher(_db);
            List<StoreSearch> result = search.FindStores(shoppingList);

            List<StoreSearch> cheapestOptions = new List<StoreSearch>();

            foreach (var storeSearch in result)
            {
                cheapestOptions.Add(storeSearch);
            }

            var options = new ShoppingOptions()
            {
                Cheapest = cheapestOptions,
                Best = null,
                Nearest = null
            };

            return options;
            
        }
    }
    

    //public class ShoppingOption
    //{
    //    public string StoreName { get; set; }
    //    public double TotalPrice { get; set; }
    //    public double TotalDistance { get; set; }
    //    public List<ProductSearch> Products { get; set; }
    //}
}