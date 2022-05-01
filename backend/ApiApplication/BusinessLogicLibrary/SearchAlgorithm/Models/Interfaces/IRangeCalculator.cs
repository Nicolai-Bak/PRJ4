using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces
{
    public interface IRangeCalculator
    {
        public double ToRadians(double angleIn10thofaDegree);
        public double Distance(double lat1, double lat2, double lon1, double lon2);
    }
}