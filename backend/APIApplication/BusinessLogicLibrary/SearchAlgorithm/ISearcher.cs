using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm.Models;

namespace ApiApplication.SearchAlgorithm
{
    public interface ISearcher
    {
        public List<StoreSearch> FindStores(ShoppingList shoppingList);
    }
}
