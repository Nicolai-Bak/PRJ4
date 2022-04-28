using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
using DatabaseLibrary;
using DatabaseLibrary.Models;

namespace ApiApplication.SearchAlgorithm
{
    public class SearchControl : ISearchControl
    {
        private List<IStoreSelecter> _storeSelecters;
        private ShoppingList _shoppingList;
        private IDbSearch _database;

        
        public SearchControl(ShoppingList shoppingList, IDbSearch database, List<IStoreSelecter> storeSelecters)
        {
            _storeSelecters = storeSelecters;
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
                AddCheapestProductToStores(storeIDs, stores, productToAdd, _shoppingList.Products[i].Unit);
            }
            
            //Tilføje data om butikkerne
            List<Store> topStoreData = _database.GetDataFromStores(storeIDs);
            AddStoredata(topStoreData, stores, _shoppingList.X, _shoppingList.Y);

           
            List<List<StoreSearch>> result = new List<List<StoreSearch>>();
            
            //Udvælg bedste butikker
            foreach (IStoreSelecter storeSelecter in _storeSelecters)
            {
                result.Add(storeSelecter.SelectStores(stores));
            }

            return result;
        }

        #region Add product to stores
        private void AddCheapestProductToStores(List<int> storeIDs, List<StoreSearch> stores, List<Product> productToAdd, double amountToGet)
        {
            // Tilføj dee billigste products i butikken til hver store - fjern store hvis den ikke har alle varer
        
            foreach (int storeID in storeIDs.ToList())
            {
                double price = 0;
                int amount = 0;
                Product temp = productToAdd
                    .Where(p => p.ProductStores
                        .Select(ps => ps.StoreKey)
                        .Any(sk => sk == storeID))
                    .MinBy(p => p.ProductStores
                        .Select(ps => GetPrice(amountToGet,ps.Product.Units,ps.Price,ref price,ref amount)));
                //.MinBy(p => p.ProductStores.Select(ps => ps.Price));
                if (temp != null)
                {
                    stores.Find(s => s.StoreID == storeID).Add(new ProductSearch(temp, price, amount));
                }
                else
                {
                    stores.Remove(stores.Find(s => s.StoreID == storeID));
                    storeIDs.Remove(storeID);
                }
            }
        }
        
        private double GetPrice(double unitsToGet, double unitsPerPiece, double pricePerPiece, ref double price, ref int amount)
        {
            amount = (int)Math.Ceiling(unitsToGet / unitsPerPiece);
            price = amount * pricePerPiece;
            return price;
        }
        #endregion

        #region Add Storedata to stores
        public void AddStoredata(List<Store> storeData, List<StoreSearch> stores, double userLocationX, double userLocationY)
        {
            foreach (Store store in storeData)
            {
                foreach (var storeSearch in stores)
                {
                    if (storeSearch.StoreID == store.ID)
                    {
                        storeSearch.Address = store.Address;
                        storeSearch.Brand = store.Brand;
                        storeSearch.Distance = GetDistance(store.Location_X, store.Location_Y, userLocationX, userLocationY);
                    }
                }
            }
        }
        
        private double GetDistance(double storeX, double storeY, double customerX, double customerY)
        {
            return Math.Sqrt(
                Math.Pow(Math.Abs(storeX - customerX), 2) +
                Math.Pow(Math.Abs(storeY - customerY), 2));
        }
        #endregion
    }
}
