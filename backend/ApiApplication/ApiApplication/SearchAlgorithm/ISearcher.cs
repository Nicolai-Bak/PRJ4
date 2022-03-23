using ApiApplication.SearchAlgorithm.Models;

namespace ApiApplication.SearchAlgorithm
{
    public interface ISearcher
    {
        public StoreSearch FindStore(List<string> productNames, double xCoordinate, double yCoordinate, int range);
    }
}
