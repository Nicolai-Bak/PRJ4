using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApiLibrary.ExternalAPIComponent.Filters
{
    public interface IFilter
    {
        public string Filter(string s);
    }
}
