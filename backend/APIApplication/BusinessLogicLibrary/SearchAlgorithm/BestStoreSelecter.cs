using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiApplication.SearchAlgorithm.Models;

namespace BusinessLogicLibrary.SearchAlgorithm
{
    public class BestStoreSelecter : IStoreSelecter
    {
        public List<StoreSearch> SelectStores(List<StoreSearch> viableStores)
        {
            //TODO
            return viableStores.Take(5).ToList();
        }
    }
}
