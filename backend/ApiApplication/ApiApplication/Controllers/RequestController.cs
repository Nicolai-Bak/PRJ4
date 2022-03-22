using System.Runtime.InteropServices;
using ApiApplication.Database;
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
        public async Task<ShoppingOptions> GetOptions(List<string> shoppingList)
        {
            ProductDTO product = new ProductDTO()
            {
                Name = "GRAASTEN ITALIENSKSALAT",
                Name2 = "150 g",
                Price = 12.95
            };

            ProductDTO product2 = new ProductDTO()
            {
                Name = "GRAASTEN ITALIENSKSALAT",
                Name2 = "150 g",
                Price = 12.95
            };

            var options = new ShoppingOptions()
            {
                Best = new ShoppingOption()
                {
                    StoreName = "Rema",
                    TotalPrice = "123",
                    TotalDistance = "2km",
                    Products = new List<ProductDTO>()
                    {
                        product,
                        product2
                    }
                }
            };
            options.Cheapest = options.Nearest = options.Best;

            return options;
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
        public string TotalPrice { get; set; }
        public string TotalDistance { get; set; }
        public List<ProductDTO> Products { get; set; }
    }

    public class ShoppingOptions
    {
        public ShoppingOption Best { get; set; }
        public ShoppingOption Cheapest { get; set; }
        public ShoppingOption Nearest { get; set; }
    }
}