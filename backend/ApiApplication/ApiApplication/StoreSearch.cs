using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiApplication.Database.Models;

namespace Sorteringsalgoritme
{
    public class StoreSearch
    {
        
        public int StoreID { get; set; }
        public List<ProductSearch> Products { get; set; }

        public StoreSearch(int id)
        {
            StoreID = id;
        }

        public float getTotal()
        {
            float total = 0;
            foreach (var product in Products)
            {
                total += (float)product.Price;
            }
            return total;
        }
    }
}
