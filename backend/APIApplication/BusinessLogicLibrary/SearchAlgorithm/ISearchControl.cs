using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm.Models;

namespace ApiApplication.SearchAlgorithm
{
    public interface ISearchControl
    {
        public List<List<StoreSearch>> FindStores(ShoppingList shoppingList);
    }
}
