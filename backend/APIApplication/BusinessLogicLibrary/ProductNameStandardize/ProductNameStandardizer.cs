using DatabaseLibrary.Models;

namespace BusinessLogicLibrary.ProductNameStandardize;

public class ProductNameStandardizer
{
    public List<ProductStandardName> Standardize(List<Product> products)
    {
        var standardList = new List<ProductStandardName>();

        products.ForEach(p =>
        {
            p.Name = p.Name
                .ToLower()
                .Replace("økologiske", "")
                .Replace("økologisk og laktosefri", "")
                .Replace("økologisk", "")
                .Replace("øko", "")
                .Replace("frisk dansk", "")
                .Replace("  ", " ")
                .Trim();
        });

        products.ForEach(p =>
        {
            var oldPsn = new ProductStandardName();
            var psn = new ProductStandardName()
            {
                Name = p.Name,
                MeasureG = (p.Measurement.ToLower().Contains("g") ? true : false),
                MeasureL = (p.Measurement.ToLower().Contains("l") ? true : false),
                MeasureStk = (p.Measurement.ToLower().Contains("stk") ? true : false),
                Organic = p.Organic
            };
            if (standardList.Any(sn => sn.Name == p.Name))
            {
                oldPsn = standardList.Find(sn => sn.Name == p.Name);
            }
            else if (standardList.Any(sn => sn.Name.Contains(p.Name + " ")))
            {
                oldPsn = standardList.Find(sn => sn.Name.Contains(p.Name + " "));
            }
            else if (standardList.Any(sn => p.Name.Contains(sn.Name + " ")))
            {
                oldPsn = standardList.Find(sn => p.Name.Contains(sn.Name + " "));
                psn.Name = oldPsn.Name;
            }
            psn.MeasureG |= oldPsn.MeasureG;
            psn.MeasureL |= oldPsn.MeasureL;
            psn.MeasureStk |= oldPsn.MeasureStk;
            psn.Organic |= oldPsn.Organic;

            standardList.Remove(oldPsn);
            standardList.Add(psn);
        });
        return standardList;
    }
}