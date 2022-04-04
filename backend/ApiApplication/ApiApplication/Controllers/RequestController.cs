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
            StoreSearch result = search.FindStore(shoppingList);

            var options = new ShoppingOptions()
            {
                Cheapest = new ShoppingOption()
                {
                    StoreName = result.StoreID.ToString(),
                    TotalPrice = result.GetTotalPrice(),
                    TotalDistance = result.Distance,
                    Products = new List<ProductDTO>()
                }
            };

            foreach (var p in result.Products)
            {
                options.Cheapest.Products.Add(new ProductDTO()
                {
                    Name = p.Name,
                    Name2 = p.Brand,
                    Price = p.Price
                });
            }

            return options;
            
            // ProductDTO product = new ProductDTO()
            // {
            //     Name = "GRAASTEN ITALIENSKSALAT",
            //     Name2 = "150 g",
            //     Price = 12.95
            // };
            //
            // ProductDTO product2 = new ProductDTO()
            // {
            //     Name = "GRAASTEN ITALIENSKSALAT",
            //     Name2 = "150 g",
            //     Price = 12.95
            // };
            //
            // var options = new ShoppingOptions()
            // {
            //     Best = new ShoppingOption()
            //     {
            //         StoreName = "Rema",
            //         TotalPrice = "123",
            //         TotalDistance = "2km",
            //         Products = new List<ProductDTO>()
            //         {
            //             product,
            //             product2
            //         }
            //     }
            // };
            // options.Cheapest = options.Nearest = options.Best;
            //
            // return options;
        }
    }

    public class ProductDTO
    {
        public string Name { get; set; }
        public string Name2 { get; set; }
        public double Price { get; set; }
    }

    public class ShoppingOption
    {
        public string StoreName { get; set; }
        public float TotalPrice { get; set; }
        public float TotalDistance { get; set; }
        public List<ProductDTO> Products { get; set; }
    }

    public class ShoppingOptions
    {
        public ShoppingOption Best { get; set; }
        public ShoppingOption Cheapest { get; set; }
        public ShoppingOption Nearest { get; set; }
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