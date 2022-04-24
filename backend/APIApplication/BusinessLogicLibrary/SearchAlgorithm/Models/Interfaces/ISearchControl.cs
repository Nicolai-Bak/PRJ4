using ApiApplication.Controllers;
using ApiApplication.Database;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;

namespace ApiApplication.SearchAlgorithm
{
    public interface ISearchControl
    {
        public List<List<StoreSearch>> FindStores();
    }
}
