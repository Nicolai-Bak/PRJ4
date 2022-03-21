using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisninjaDatabaseAPI
{
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public Item[] Items { get; set; }
    }
}
