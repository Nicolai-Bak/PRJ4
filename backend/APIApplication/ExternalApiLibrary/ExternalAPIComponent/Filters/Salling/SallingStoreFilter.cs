using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters.Salling;
public class SallingStoreFilter : IFilter
{
    public List<object> Filter(List<object> o)
    {
        List<FilteredSallingStore> json = JsonConvert.DeserializeObject<List<FilteredSallingStore>>(JsonConvert.SerializeObject(o))!;

        return new List<object>(json);
    }
}