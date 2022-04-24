using ApiApplication.Controllers;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

namespace ApiApplication.SearchAlgorithm
{
    public class SearchControl : ISearchControl
    {
        private List<IStoreSelecter> _storeSelecters;
        private IProductAdder _productAdder;
        private IStoredataAdder _storedataAdder;
        private ShoppingList _shoppingList;
        private IPrisninjaDB _database;

        
        public SearchControl(ShoppingList shoppingList, IPrisninjaDB database, List<IStoreSelecter> storeSelecters, IProductAdder productAdder, IStoredataAdder storedataAdder)
        {
            _storeSelecters = storeSelecters;
            _productAdder = productAdder;
            _storedataAdder = storedataAdder;
            _shoppingList = shoppingList;
            _database = database;
        }

        public List<List<StoreSearch>> FindStores()
        {
            //1. Create list of StoreSearchs
            List<int> storeIDs = _database.GetStoresInRange(_shoppingList.X, _shoppingList.Y, _shoppingList.Range);
            List<StoreSearch> stores = new List<StoreSearch>();

            foreach (var storeID in storeIDs)
            {
                stores.Add(new StoreSearch(storeID));
            }
            
            
            // Tilføj dee billigste products i butikken til hver store - fjern store hvis den ikke har alle varer
            for (int i = 0; i < _shoppingList.Products.Count(); i++)
            {
                List<Product> productToAdd = _database.GetProductsFromSpecificStores(storeIDs, _shoppingList.Products[i].Name, _shoppingList.Products[i].Measurement);
                _productAdder.AddCheapestProductToStores(storeIDs, stores, productToAdd, _shoppingList.Products[i].Unit);
            }
            
            //Tilføje data om butikkerne
            List<Store> topStoreData = _database.GetDataFromStores(storeIDs);
            _storedataAdder.AddStoredata(topStoreData, stores, _shoppingList.X, _shoppingList.Y);

           
            List<List<StoreSearch>> result = new List<List<StoreSearch>>();
            
            //Udvælg bedste butikker
            foreach (IStoreSelecter storeSelecter in _storeSelecters)
            {
                result.Add(storeSelecter.SelectStores(stores));
            }

            return result;
        }
        
        

        
    }
}
