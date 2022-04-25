using System.Runtime.InteropServices;
using ApiApplication.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
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
        public async Task<IShoppingOptions> GetOptions(ShoppingList shoppingList)
        {
            //Setup
            List<IStoreSelecter> storeSelecters = new List<IStoreSelecter>
            {
                new CheapestStoreSelecter(),
                new ClosestStoreSelecter(),
                new BestStoreSelecter()
            };
            ISearchControl search = new SearchControl(shoppingList, (IDbSearch)_db, storeSelecters);

            return new ShoppingOptionsDummyData();
            
        }
    }
}