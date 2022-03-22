using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseApi;

namespace Sorteringsalgoritme.SearchAlgorithm
{
    public class CheapestSeacher : ISearcher
    {
        private DatabaseReader database = new DatabaseReader();
        public Store findStore(List<string> productNames, int xCoordinate, int yCoordinate)
        {
            //1. Create list of stores
            List<Store> stores = new List<Store>();

            //2. Get Products from area
            List<Product> products = database.getProductsFromArea(productNames[0], xCoordinate, yCoordinate);

            //3.1 Tilføj manglende stores
            foreach (Product product in products)
            {
                if (!stores.Select(s => s.StoreID).Contains(product.StoreID))
                {
                    stores.Add(new Store(product.StoreID));
                }
            }

            // Tilføj dee billigste products i butikken til hver store - fjern store hvis den ikke har alle varer
            for (int i = 0; i < productNames.Count(); i++)
            {
                List<Product> productsToAdd = database.getProductsFromStores(productNames[i], stores);
                foreach (Store store in stores.ToList())
                {
                    Product temp = products.Where(p => p.StoreID == store.StoreID).MinBy(p => p.Price);
                    if (temp != null)
                    {
                        store.Products[0] = temp;
                    }
                    else
                    {
                        stores.Remove(store);
                    }
                }
            }

            return stores.MinBy(s => s.getTotal());
        }
    }
}
