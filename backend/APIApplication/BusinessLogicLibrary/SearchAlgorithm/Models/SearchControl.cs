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
        private readonly IRangeCalculator _rangeCalculator;

        
        public SearchControl(ShoppingList shoppingList, IDbSearch database, List<IStoreSelecter> storeSelecters, IRangeCalculator rangeCalculator)
        {
            _storeSelecters = storeSelecters;
            _shoppingList = shoppingList;
            _database = database;
            _rangeCalculator = rangeCalculator;
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
                
                List<Product> tempProducts = productToAdd
                    .Where(p => p.ProductStores
                        .Select(ps => ps.StoreKey)
                        .Any(sk => sk == storeID)).ToList();
                Product temp = null;
                double price = int.MaxValue;
                int amount = 0;
                foreach (var tempProduct in tempProducts)
                {
                    (double tempPrice, int tempAmount) res = GetPrice(amountToGet, tempProduct.Units, tempProduct.ProductStores.Find(ps => ps.StoreKey == storeID).Price);
                    if (res.tempPrice < price)
                    {
                        temp = tempProduct;
                        price = res.tempPrice;
                        amount = res.tempAmount;
                    }
                }
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
        
        private (double price, int amount) GetPrice(double unitsToGet, double unitsPerPiece, double pricePerPiece)
        {
            int amount = (int)Math.Ceiling(unitsToGet / unitsPerPiece);
            double price = amount * pricePerPiece;
            return (price, amount);
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
           
            return _rangeCalculator.Distance(storeX, customerX, storeY, customerY);
        }
        #endregion
    }
}
