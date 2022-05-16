using System.Runtime.InteropServices;
using ApiApplication.SearchAlgorithm;
using BusinessLogicLibrary.ProductNameStandardize;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IDbRequest _db;
        private readonly IRangeCalculator _rangeCalculator;

        public RequestController(IDbRequest db, IRangeCalculator rangeCalculator)
        {
            _db = db;
            _rangeCalculator = rangeCalculator;
        }

        [HttpGet("/update")] // Api call for testing External Api hosted service
        public void UpdateDatabase()
        {
            ExternalApiService service = new ExternalApiService((IDbInsert) _db,
                new Dictionary<IApiFactory, IApiFactory>()
                {
                    {
                        new FoetexProductFactory(), new FoetexStoreFactory()
                    },
                    {
                        new CoopProductFactory(), new CoopStoreFactory()
                    }
                },
                new ProductNameStandardizer(),
                true);
            service.UpdateDatabase();
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
            ISearchControl search = new SearchControl(shoppingList, (IDbSearch) _db, storeSelecters, _rangeCalculator);

            return new ShoppingOptions(search);
        }
    }
}