using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Models;
using Newtonsoft.Json;

namespace ExternalApiLibrary.Filters.Salling;
public class SallingStoreFilter : IFilter
{
    public List<object> Filter(List<object> o)
    {
        List<FilteredSallingStore> json = JsonConvert.DeserializeObject<List<FilteredSallingStore>>(JsonConvert.SerializeObject(o))!;

        return new List<object>(json);
    }
}