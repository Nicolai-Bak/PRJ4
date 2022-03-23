using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseApi;
using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;

namespace Sorteringsalgoritme.SearchAlgorithm
{
    public class CheapestSeacher : ISearcher
    {
        private PrisninjaDb database = new PrisninjaDb(new PrisninjaDbContext());
        public StoreSearch FindStore(List<string> productNames, int xCoordinate, int yCoordinate, int range)
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
                        stores.Find(s => s.StoreID == storeID).Products[i] = temp;
                    }
                    else
                    {
                        stores.Remove(stores.Find(s => s.StoreID == storeID));
                        storeIDs.Remove(storeID);
                    }
                }
            }

            return stores.MinBy(s => s.getTotal());
        }
    }
}
