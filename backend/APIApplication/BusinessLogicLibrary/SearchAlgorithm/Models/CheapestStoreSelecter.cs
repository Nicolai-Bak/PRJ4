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
        /*
         * @Arguments: List of StoreSearch objects that contains live up to the requirements for the search
         * @Returns: The five StoreSearch object with the lowest total price in order from cheapest to most expensive
         */
        public List<StoreSearch> SelectStores(List<StoreSearch> viableStores)
        {
            var res = viableStores.OrderBy(s => s.TotalPrice).ThenBy(s=>s.Distance).Take(5).ToList();

            return res;
        }
    }
}
