using System.Runtime.InteropServices;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IDbRequest _db;

        public RequestController(IDbRequest db)
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
            List<StoreSearch> result = _search.FindStores(shoppingList);

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
}