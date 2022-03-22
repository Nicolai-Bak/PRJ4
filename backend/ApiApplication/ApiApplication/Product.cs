using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteringsalgoritme
{
    public class Product
    {
        public int StoreID { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public int Units { get; set; }
        public string UnitOfMeasurement { get; set; }

    }
}
