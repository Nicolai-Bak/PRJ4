using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary;

    public interface IRangeCalculator
    {
        public double Distance(double lat1, double lat2, double lon1, double lon2);
    }
