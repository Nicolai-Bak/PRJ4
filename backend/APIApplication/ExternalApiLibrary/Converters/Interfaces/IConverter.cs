using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApiLibrary.Converters.Interfaces
{
    public interface IConverter
    {
        public List<object> Convert(List<object> list);
    }
}
