using ApiApplication.Database;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]

    public class OptionsController : Controller
    {
        private readonly IPrisninjaDB _repo;

        public OptionsController(IPrisninjaDB repo)
        {
            _repo = repo;
        }


        [HttpGet("/name")]
        public List<string> GetProductsByName()
        {
            return  _repo.GetAllProductNames();
        }


        [HttpGet("/options")]
        public async Task<Root> GetOptions()
        {
            var root = new Root();
            var best = new Best();
            var nearest = new Nearest();
            var cheapest = new Cheapest();
            Product2 product = new Product2();
            product.Name = "GRAASTEN ITALIENSKSALAT";
            product.Name2 = "150 g";
            product.Price = 12.95;

            Product2 product2 = new Product2();
            product2.Name = "GRAASTEN ITALIENSKSALAT";
            product2.Name2 = "150 g";
            product2.Price = 12.95;

            best.StoreName = "Rema";
            best.TotalPrice = "123";
            best.TotalDistance = "2km";
            best.Products = new List<Product2>();
            best.Products.Add(product);
            best.Products.Add(product2);
            root.Best = best;

            nearest.StoreName = "Rema";
            nearest.TotalPrice = "123";
            nearest.TotalDistance = "2km";
            nearest.Products = new List<Product2>();
            nearest.Products.Add(product);
            nearest.Products.Add(product2);
            root.Nearest = nearest;

            cheapest.StoreName = "Rema";
            cheapest.TotalPrice = "123";
            cheapest.TotalDistance = "2km";
            cheapest.Products = new List<Product2>();
            cheapest.Products.Add(product);
            cheapest.Products.Add(product2);
            root.Cheapest = cheapest;

            return root;


        }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);




    public class Product2
    {
        public string Name { get; set; }
        public string Name2 { get; set; }
        public double Price { get; set; }
    }

    public class Best
    {
        public string StoreName { get; set; }
        public string TotalPrice { get; set; }
        public string TotalDistance { get; set; }
        public List<Product2> Products { get; set; }
    }

    public class Cheapest
    {
        public string StoreName { get; set; }
        public string TotalPrice { get; set; }
        public string TotalDistance { get; set; }
        public List<Product2> Products { get; set; }
    }

    public class Nearest
    {
        public string StoreName { get; set; }
        public string TotalPrice { get; set; }
        public string TotalDistance { get; set; }
        public List<Product2> Products { get; set; }
    }

    public class Root
    {
        public Best Best { get; set; }
        public Cheapest Cheapest { get; set; }
        public Nearest Nearest { get; set; }
    }
}
