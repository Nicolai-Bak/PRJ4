using System.Runtime.InteropServices;
using ApiApplication.Database;
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


        [HttpPost("/options")]
        public async Task<ShoppingOptions> GetOptions(ShoppingList shoppingList)
        {
            CheapestSearcher search = new CheapestSearcher();
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

    public class ShoppingOptions
    {
        public List<StoreSearch> Best { get; set; }
        public List<StoreSearch> Cheapest { get; set; }
        public List<StoreSearch> Nearest { get; set; }
    }

    


    public class ShoppingList
    {
        public List<ShoppingListItem> Products { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Range { get; set; }
    }
    
    public class ShoppingListItem
    {
        public string Name { get; set; }
        public double Unit { get; set; }
        public string Measurement { get; set; }
    }
}