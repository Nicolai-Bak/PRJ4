using BusinessLogicLibrary.SearchAlgorithm.Models;

namespace BusinessLogicLibrary.SearchAlgorithm
{
    public interface ISearcher
    {
        public List<StoreSearch> FindStores(ShoppingList shoppingList);
    }
}
