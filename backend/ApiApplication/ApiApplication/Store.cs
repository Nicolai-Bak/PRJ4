using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteringsalgoritme
{
    public class Store
    {
        
        public int StoreID { get; set; }
        public List<Product> Products { get; set; }

        public Store(int id)
        {
            StoreID = id;
        }

        public float getTotal()
        {
            float total = 0;
            foreach (var product in Products)
            {
                total += product.Price;
            }
            return total;
        }
    }
}
