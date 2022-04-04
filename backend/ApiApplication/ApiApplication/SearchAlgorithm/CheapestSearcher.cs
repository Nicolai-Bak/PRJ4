using ApiApplication.Controllers;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using ApiApplication.SearchAlgorithm.Models;

namespace ApiApplication.SearchAlgorithm
{
    public class CheapestSearcher : ISearcher
    {
        private PrisninjaDb database = new PrisninjaDb(new PrisninjaDbContext());
        public StoreSearch FindStore(ShoppingList shoppingList)
        {
            //1. Create list of StoreSearchs
            List<int> storeIDs = database.GetStoresInRange(xCoordinate, yCoordinate, range);
            List<StoreSearch> stores = new List<StoreSearch>();

            foreach (var storeID in storeIDs)
            {
                stores.Add(new StoreSearch(storeID));
            }
            
            // Tilføj dee billigste products i butikken til hver store - fjern store hvis den ikke har alle varer
            for (int i = 0; i < productNames.Count(); i++)
            {
                List<Product> productsToAdd = database.GetProductsFromSpecificStores(storeIDs, productNames[i]);
                foreach (int storeID in storeIDs.ToList())
                {
                    Product temp = productsToAdd
                            .Where(p => p.ProductStores.Select(ps => ps.StoreKey)
                            .Any(sk => sk == storeID))
                            .MinBy(p => p.ProductStores.Select(ps => ps.Price));
                    if (temp != null)
                    {
                        stores.Find(s => s.StoreID == storeID).Products[i] = new ProductSearch()
                        {
                            EAN = temp.EAN,
                            Name = temp.Name,
                            Brand = temp.Brand,
                            Unit = temp.Unit,
                            Measurement = temp.Measurement,
                            Price = temp.ProductStores.Find(ps => ps.StoreKey == storeID).Price
                        };
                    }
                    else
                    {
                        stores.Remove(stores.Find(s => s.StoreID == storeID));
                        storeIDs.Remove(storeID);
                    }
                }
            }

            return new StoreSearch(2)
            {
                Products = new List<ProductSearch>()
                {
                    new ProductSearch()
                    {
                        Price = 12
                    }
                }
            };
            return stores.MinBy(s => s.GetTotalPrice());
        }
    }
}
