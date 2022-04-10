using System.Globalization;
using ApiApplication.Database.Models;

namespace ApiApplication.Database.ProductNameStandardize;

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
            var psn = new ProductStandardName()
            {
                Name = p.Name,
                MeasureG = false,
                MeasureL = false,
                MeasureStk = false,
                Organic = false,
                StandardUnits = 0
            };
            
            if (standardList.Any(sn => sn.Name.Contains(p.Name)))
            {
                standardList.Find(sl => sl.Name.Contains(p.Name)).Name = p.Name;
            }
            else if (standardList.Any(sn => p.Name.Contains(sn.Name)))
            {
                
            }
            else
            {
                standardList.Add(psn);
            }
        });
        return standardList;
    }
}