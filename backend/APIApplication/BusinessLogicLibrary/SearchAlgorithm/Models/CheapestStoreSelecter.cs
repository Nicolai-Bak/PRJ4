using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiApplication.SearchAlgorithm.Models;

namespace BusinessLogicLibrary.SearchAlgorithm
{
    public class CheapestStoreSelecter : IStoreSelecter
    {
        public List<StoreSearch> SelectStores(List<StoreSearch> viableStores)
        {
            return viableStores.OrderBy(s => s.TotalPrice).Take(5).ToList();
        }
    }
}
