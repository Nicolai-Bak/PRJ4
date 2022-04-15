using ApiApplication.Controllers;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm.Models;

namespace ApiApplication.SearchAlgorithm
{
    public class CheapestSearcher : ISearcher
    {
        private IPrisninjaDB _database = new PrisninjaDb(new PrisninjaDbContext());

        public CheapestSearcher(IPrisninjaDB database)
        {
            _database = database;
        }

        public List<StoreSearch> FindStores(ShoppingList shoppingList)
        {
            //1. Create list of StoreSearchs
            List<int> storeIDs = _database.GetStoresInRange(shoppingList.X, shoppingList.Y, shoppingList.Range);
            List<StoreSearch> stores = new List<StoreSearch>();

            foreach (var storeID in storeIDs)
            {
                stores.Add(new StoreSearch(storeID));
            }
            
            // Tilføj dee billigste products i butikken til hver store - fjern store hvis den ikke har alle varer
            for (int i = 0; i < shoppingList.Products.Count(); i++)
            {
                List<Product> productsToAdd = _database.GetProductsFromSpecificStores(storeIDs, shoppingList.Products[i].Name, shoppingList.Products[i].Measurement);
                foreach (int storeID in storeIDs.ToList())
                {
                    double price = 0;
                    int amount = 0;
                    Product temp = productsToAdd
                            .Where(p => p.ProductStores
                                .Select(ps => ps.StoreKey)
                                .Any(sk => sk == storeID))
                            .MinBy(p => p.ProductStores
                                .Select(ps => GetPrice(shoppingList.Products[i].Unit,ps.Product.Units,ps.Price,ref price,ref amount)));
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

            List<Store> topStoreData = _database.GetDataFromStores(storeIDs);
            foreach (Store store in topStoreData)
            {
                foreach (var storeSearch in stores)
                {
                    if (storeSearch.StoreID == store.ID)
                    {
                        storeSearch.Address = store.Address;
                        storeSearch.Brand = store.Brand;
                        storeSearch.Distance = GetDistance(store.Location_X, store.Location_Y, shoppingList.X, shoppingList.Y);
                    }
                }
            }

            //List<StoreSearch> sea = new List<StoreSearch>();
            //sea.Add(new StoreSearch(2)
            //{
            //    Products = new List<ProductSearch>()
            //    {
            //        new ProductSearch()
            //        {
            //            Price = 12
            //        }
            //    }
            //});
            //return sea;
            return stores.OrderBy(s => s.TotalPrice).Take(5).ToList();
        }
        
        private double GetPrice(double unitsToGet, double unitsPerPiece, double pricePerPiece, ref double price, ref int amount)
        {
            amount = (int)Math.Ceiling(unitsToGet / unitsPerPiece);
            price = amount * pricePerPiece;
            return price;
        }

        private double GetDistance(double storeX, double storeY, double customerX, double customerY)
        {
            return Math.Sqrt(
                Math.Pow(Math.Abs(storeX - customerX), 2) +
                Math.Pow(Math.Abs(storeY - customerY), 2));
        }
    }
}
