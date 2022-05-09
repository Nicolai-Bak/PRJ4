using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiApplication.SearchAlgorithm.Models;

namespace BusinessLogicLibrary.SearchAlgorithm
{
    public class ClosestStoreSelecter : IStoreSelecter
    {
        public List<StoreSearch> SelectStores(List<StoreSearch> viableStores)
        {
            return viableStores.OrderBy(s => s.Distance).ThenBy(s=>s.TotalPrice).Take(5).ToList();
        }
    }
}
