using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.SearchAlgorithm.Models;

public class RangeCalculator : IRangeCalculator
{
    public double ToRadians(double angle)
    {
        // Angle in 10th
        // of a degree
        return (angle * Math.PI) / 180;
    }

    public double Distance(double lat1, double lat2, double lon1, double lon2)
    {

        //Converts to Radians
        lon1 = ToRadians(lon1);
        lon2 = ToRadians(lon2);
        lat1 = ToRadians(lat1);
        lat2 = ToRadians(lat2);


        // Haversine formula
        double dlon = lon2 - lon1;
        double dlat = lat2 - lat1;
        double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Pow(Math.Sin(dlon / 2), 2);

        double c = 2 * Math.Asin(Math.Sqrt(a));

        // Radius of earth in kilometers.
        double r = 6371;

        // calculate the result
        return (c * r);
    }

}