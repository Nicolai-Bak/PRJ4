using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteringsalgoritme.SearchAlgorithm
{
    public interface ISearcher
    {
        public Store findStore(List<string> productNames, int xCoordinate, int yCoordinate);
    }
}
