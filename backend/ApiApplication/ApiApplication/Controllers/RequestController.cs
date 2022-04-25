using System.Runtime.InteropServices;
using ApiApplication.Database;
using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
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
            //Setup
            List<IStoreSelecter> storeSelecters = new List<IStoreSelecter>
            {
                new CheapestStoreSelecter(),
                new ClosestStoreSelecter(),
                new BestStoreSelecter()
            };
            IProductAdder productAdder = new ProductAdder();
            IStoredataAdder storedataAdder = new StoredataAdder();
            ISearchControl search = new SearchControl(shoppingList, _db, storeSelecters, productAdder, storedataAdder);



            return new ShoppingOptions(search);
            
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