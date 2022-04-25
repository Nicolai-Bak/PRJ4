using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces
{
    public interface IConverter
    {
        public List<object> Convert(List<object> list);
    }
}
