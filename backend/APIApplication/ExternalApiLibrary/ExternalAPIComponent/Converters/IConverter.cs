using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters
{
    public interface IConverter
    {
        public string Convert(string s);
    }
}
